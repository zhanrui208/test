using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace JERPApp.Store.MaterialOEM.Report.Bill
{
    public class FrmBizBill:Form 
    {
       public virtual void DetailNote(long NoteID)
       {
       }
       public const int CheckStoreNoteID = 1;
       public const string CheckStoreNoteName = "�̵㵥";
       public const int ReceiveNoteID = 2;
       public const string ReceiveNoteNoteName = "�ջ���";
       public const int ReturnNoteID = 3;
       public const string ReturnNoteNoteName = "�˻���";
       public const int OutStoreNoteID =4;
       public const string OutStoreNoteName = "���ⵥ";
       public const int IntoStoreNoteID = 5;
       public const string IntoStoreNoteName = "��ⵥ"; 

       public const int OutSrcSupplyNoteID = 6;
       public const string OutSrcSupplyNoteName = "ί�ⷢ�ϵ�";
       public const int OutSrcRecycleNoteID = 7;
       public const string OutSrcRecycleNoteName = "ί����ϵ�";   
   
       public static FrmBizBill Create(int NoteNameID)
       {
           switch (NoteNameID)
           {
               case CheckStoreNoteID: return new FrmStoreCheckNote();
               case ReceiveNoteID: return new FrmReceiveNote();
               case ReturnNoteID: return new FrmReturnNote();
               case OutStoreNoteID: return new FrmOutStoreNote(); 
               case IntoStoreNoteID: return new FrmIntoStoreNote();
               case OutSrcSupplyNoteID: return new FrmOutSrcSupplyNote();
               case OutSrcRecycleNoteID: return new FrmOutSrcRecycleNote();
               default: return null;                   
            }
       }

    }
}
