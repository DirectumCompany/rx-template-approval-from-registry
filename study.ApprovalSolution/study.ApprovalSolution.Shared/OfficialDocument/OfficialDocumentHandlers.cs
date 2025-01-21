using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using study.ApprovalSolution.OfficialDocument;

namespace study.ApprovalSolution
{
  partial class OfficialDocumentSharedHandlers
  {
    
    public override void LifeCycleStateChanged(Sungero.Domain.Shared.EnumerationPropertyChangedEventArgs e)
    {
      base.LifeCycleStateChanged(e);
      //Если состояние прекращено, очищаем срок, очищаем исполнителей задачи
      if (_obj.LifeCycleState == OfficialDocument.LifeCycleState.Obsolete)
      {
        _obj.JobDate = null;
        _obj.TaskParticipants.Clear();
      }
    }
  }
}