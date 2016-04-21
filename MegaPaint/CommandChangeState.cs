using System.Collections.Generic;

namespace MegaPaint
{
    // Lớp CommandChangeState kế thừa từ Command
    internal class CommandChangeState : Command
    {
        private List<DrawObject> listBefore;
        private List<DrawObject> listAfter;
        private int activeLayer;
        
        // Command Change State
        public CommandChangeState(Layers layerList)
        {
            activeLayer = layerList.ActiveLayerIndex;
            FillList(layerList[activeLayer].Graphics, ref listBefore);
        }

        public void NewState(Layers layerList)
        {
            FillList(layerList[activeLayer].Graphics, ref listAfter);
        }

        // Replace phục vụ cho Undo và Redo
        private void ReplaceObjects(GraphicsList graphicsList, List<DrawObject> list)
        {
            for (int i = 0; i < graphicsList.Count; i++)
            {
                DrawObject replacement = null;

                foreach (DrawObject o in list)
                {
                    if (o.ID ==
                        graphicsList[i].ID)
                    {
                        replacement = o;
                        break;
                    }
                }

                if (replacement != null)
                {
                    graphicsList.Replace(i, replacement);
                }
            }
        }

        // Thêm GraphicList được select vào FillList
        private void FillList(GraphicsList graphicsList, ref List<DrawObject> listToFill)
        {
            listToFill = new List<DrawObject>();

            foreach (DrawObject o in graphicsList.Selection)
            {
                listToFill.Add(o.Clone());
            }
        }

        // Undo và Redo cho trường hợp đối tượng thay đổi một trạng thái nào đó (Phóng to, Thu nhỏ, Di chuyển)
        public override void Undo(Layers list)
        {
            ReplaceObjects(list[activeLayer].Graphics, listBefore);
        }

        public override void Redo(Layers list)
        {
            ReplaceObjects(list[activeLayer].Graphics, listAfter);
        }
    }
}
