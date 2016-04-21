using System.Collections.Generic;

namespace MegaPaint
{
    // Lớp CommandDelete kế thừa từ Command
    internal class CommandDelete : Command
    {
        private List<DrawObject> cloneList; // Chứa danh sách object đã select

        // Command Delete
        public CommandDelete(Layers list)
        {
            cloneList = new List<DrawObject>();

            // Clone danh sách object đã được select trong layer
            foreach (DrawObject o in list[list.ActiveLayerIndex].Graphics.Selection)
                cloneList.Add(o.Clone());
        }

        // Undo và Redo cho trường hợp một đối tượng bị xóa đi
        public override void Undo(Layers list)
        {
            list[list.ActiveLayerIndex].Graphics.UnselectAll();
            foreach (DrawObject o in cloneList)
            {
                list[list.ActiveLayerIndex].Graphics.Add(o);
            }
        }

        public override void Redo(Layers list)
        {
            int n = list[list.ActiveLayerIndex].Graphics.Count;

            for (int i = n - 1; i >= 0; i--)
            {
                bool toDelete = false;
                DrawObject objectToDelete = list[list.ActiveLayerIndex].Graphics[i];

                foreach (DrawObject o in cloneList)
                {
                    if (objectToDelete.ID ==
                        o.ID)
                    {
                        toDelete = true;
                        break;
                    }
                }

                if (toDelete)
                {
                    list[list.ActiveLayerIndex].Graphics.RemoveAt(i);
                }
            }
        }
    }
}
