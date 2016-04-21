namespace MegaPaint
{
    // Lớp Base Command
    public abstract class Command
    {
        // Hai phương thức Undo và Redo có thể kế thừa
        public abstract void Undo(Layers list);
        public abstract void Redo(Layers list);
    }
}
