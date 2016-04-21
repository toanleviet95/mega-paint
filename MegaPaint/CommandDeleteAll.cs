using System.Collections.Generic;

namespace MegaPaint
{
    // Lớp CommandDeleteAll kế thừa từ Command
    internal class CommandDeleteAll : Command
    {
        private List<DrawObject> cloneList; // Chứa danh sách object đã select

        // Command Delete All
        public CommandDeleteAll(Layers list)
        {
            cloneList = new List<DrawObject>();

            // Clone danh sách toàn bộ object trên layer
            int n = list[list.ActiveLayerIndex].Graphics.Count;

            for (int i = n - 1; i >= 0; i--)
            {
                cloneList.Add(list[list.ActiveLayerIndex].Graphics[i].Clone());
            }
        }

        // Undo và Redo cho trường hợp tất cả đối tượng bị xóa
        public override void Undo(Layers list)
        {
            foreach (DrawObject o in cloneList)
            {
                list[list.ActiveLayerIndex].Graphics.Add(o);
            }
        }

        public override void Redo(Layers list)
        {
            list[list.ActiveLayerIndex].Graphics.Clear();
        }
    }
}
