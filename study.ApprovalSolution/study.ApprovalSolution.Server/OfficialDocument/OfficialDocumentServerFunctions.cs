using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using study.ApprovalSolution.OfficialDocument;

namespace study.ApprovalSolution.Server
{
  partial class OfficialDocumentFunctions
  {
    
    /// <summary>
    /// Отправить на согласование.
    /// </summary>
    public virtual void SetOnApproval()
    {
      if (_obj.ResponsibleDirRX == null)
        _obj.ResponsibleDirRX = Sungero.Company.Employees.Current;
      
      //Если ответственный-руководитель, добавляем доп. согласующих в список исполнителей. Иначе добавляем руководителя ответственного.
      if (Sungero.Company.PublicFunctions.Employee.IsManager(_obj.ResponsibleDirRX))
      {
        SetApprovedManager();
      }
      else
      {
        _obj.JobDate = Calendar.Now.AddWorkingDays(1);
        _obj.InternalApprovalState = InternalApprovalState.ApproveBoss;
        var manager = _obj.ResponsibleDirRX.Department.Manager;
        
        if (manager != null)
          _obj.TaskParticipants.AddNew().Executor = manager;
      }
    }
    
    /// <summary>
    /// Выполнить этап Согласовать.
    /// </summary>
    public virtual void SetApproved()
    {
      _obj.JobDate = Calendar.Now.AddWorkingDays(1);
      _obj.InternalApprovalState = InternalApprovalState.PendingSign;
      _obj.TaskParticipants.AddNew().Executor = _obj.OurSignatory;
    }
    
    /// <summary>
    /// Выполнить этап Согласовать руководителем.
    /// </summary>
    public virtual void SetApprovedManager()
    {
      if (_obj.AddApprovers.Count > 0)
      {
        _obj.JobDate = Calendar.Now.AddWorkingDays(1);
        _obj.InternalApprovalState = InternalApprovalState.OnApproval;
        
        foreach (var approver in _obj.AddApprovers)
          _obj.TaskParticipants.AddNew().Executor = approver.Approver;
      }
      else
      {
        SetApproved();
      }
    }
    
    /// <summary>
    /// Выполнить этап Подписано.
    /// </summary>
    public virtual void SetSign()
    {
      _obj.JobDate = null;
      _obj.TaskParticipants.Clear();
    }
    
    /// <summary>
    /// Выполнить этап На доработку.
    /// </summary>
    public virtual void SetRework()
    {
      _obj.TaskParticipants.Clear();
      _obj.InternalApprovalState = InternalApprovalState.OnRework;
      _obj.JobDate = Calendar.Now.AddWorkingDays(1);
      _obj.TaskParticipants.AddNew().Executor = _obj.ResponsibleDirRX;
      _obj.Save();
      
    }
    
    /// <summary>
    /// Выполнить следующий этап процесса.
    /// </summary>
    [Remote]
    public virtual void NextStep()
    {
      // Если все текущие исполнители выполнили задание, то переход к следующему этапу.
      if (!_obj.TaskParticipants.Any(p => string.IsNullOrEmpty(p.Result)))
      {
        // Если есть хоть один результат на доработку, то переходим на этап На доработку.
        if (_obj.TaskParticipants.Any(p => p.Result == Constants.Docflow.OfficialDocument.ReworkResult))
        {
          SetRework();
          return;
        }
        // Переходим на этап Согласование.
        if (_obj.InternalApprovalState == InternalApprovalState.ApproveBoss)
        {
          SetApprovedManager();
        }
        // Переходим на этап На подписании.
        else if (_obj.InternalApprovalState == InternalApprovalState.OnApproval)
        {
          SetApproved();
        }
        // Переходим на этап Подписано.
        else if (_obj.InternalApprovalState == InternalApprovalState.Signed)
        {
          SetSign();
        }
        // Отправляем на согласование.
        else
        {
          SetOnApproval();
        }
      }
      _obj.Save();
    }
  }
}