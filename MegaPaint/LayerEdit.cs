namespace MegaPaint
{
    public class LayerEdit
    {
        private string _name;
        private bool _active;
        private bool _visible;
        private bool _delete;
        private bool _new;
        
        public string LayerName
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool LayerActive
        {
            get { return _active; }
            set { _active = value; }
        }

        public bool LayerVisible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public bool LayerDelete
        {
            get { return _delete; }
            set { _delete = value; }
        }

        public bool LayerNew
        {
            get { return _new; }
            set { _new = value; }
        }
    }
}
