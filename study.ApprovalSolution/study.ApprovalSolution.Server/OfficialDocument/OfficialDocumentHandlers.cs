using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using study.ApprovalSolution.OfficialDocument;

namespace study.ApprovalSolution
{

  partial class OfficialDocumentServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      //Заполянем текущего исполнителя из таблицы исполнителей задачи.
      var taskParticipants = string.Join(", ", _obj.TaskParticipants.Where(p => string.IsNullOrEmpty(p.Result)).Select(p => p.Executor?.DisplayValue));
      _obj.CurrentExecutors = Sungero.Docflow.PublicFunctions.Module.CutText(taskParticipants, _obj.Info.Properties.CurrentExecutors.Length);
    }
  }
}