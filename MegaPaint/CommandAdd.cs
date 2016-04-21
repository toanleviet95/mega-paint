namespace MegaPaint
{
    // Lớp CommandAdd kế thừa từ Command
    internal class CommandAdd : Command
    {
        private DrawObject drawObject;

        // Command Add
        public CommandAdd(DrawObject drawObject) : base()
        {
            this.drawObject = drawObject.Clone();
        }

        // Undo và Redo cho trường hợp một đối tượng mới được thêm vào DrawArea
        public override void Undo(Layers list)
        {
            list[list.ActiveLayerIndex].Graphics.DeleteLastAddedObject();
        }

        public override void Redo(Layers list)
        {
            list[list.ActiveLayerIndex].Graphics.UnselectAll();
            list[list.ActiveLayerIndex].Graphics.Add(drawObject);
        }
    }
}
