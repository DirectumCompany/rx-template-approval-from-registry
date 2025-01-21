using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using study.ApprovalSolution.OfficialDocument;

namespace study.ApprovalSolution.Shared
{
  partial class OfficialDocumentFunctions
  {
    /// <summary>
    /// Установить комментарий в переписку.
    /// </summary>
    /// <param name="comment"></param>
    public virtual void SetComment(string comment, string result)
    {
      var newText = string.Format("**************{0}({1}) {2}**************{3}{4}{3}",
                                  Sungero.Company.Employees.Current.DisplayValue, result, Calendar.Now.ToString(), Environment.NewLine, comment);
      _obj.TaskText = string.Join(string.Empty, _obj.TaskText, newText);
    }
    
    /// <summary>
    /// Установить результат в таблицу исполнителей.
    /// </summary>
    public virtual void SetResult(string result)
    {
      var row = _obj.TaskParticipants.Where(p => Equals(p.Executor, Sungero.Company.Employees.Current)).LastOrDefault();
      if (row != null)
        row.Result = result;
    }
  }
}