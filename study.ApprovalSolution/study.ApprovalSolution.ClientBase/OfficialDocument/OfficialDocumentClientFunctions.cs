using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using study.ApprovalSolution.OfficialDocument;

namespace study.ApprovalSolution.Client
{
  partial class OfficialDocumentFunctions
  {
   /// <summary>
   /// Функция показывает окно с коментарием
   /// </summary>
   /// <param name="required">Обязательность комментария</param>
   /// <param name="autotext">Текст по умолчанию</param>
   /// <returns></returns>
    public static string ShowCommentDialog(bool required, string autotext)
    {
      var dialog = Dialogs.CreateInputDialog(study.ApprovalSolution.OfficialDocuments.Resources.Comment);
      var comment = dialog.AddMultilineString(study.ApprovalSolution.OfficialDocuments.Resources.Comment, required);
      comment.Value = autotext;
      dialog.Buttons.AddOkCancel();
      if (dialog.Show() == DialogButtons.Ok)
      {
        return comment.Value.ToString();
      }
      return Constants.Docflow.OfficialDocument.CancelDialogResult;
    }
  }
}