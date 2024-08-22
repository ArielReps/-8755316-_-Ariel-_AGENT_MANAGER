namespace FinalProjectAPI.Models.AuxiliaryModels
{
    public class Direction
    {
        public string direction {  get; set; }
        public int Horizontal
        {
            get
            {
                if (direction.Contains('e')) return 1;
                if (direction.Contains('w')) return -1;
                return 0;
            }
        }
        public int Vertical
        {
            get
            {
                if (direction.Contains('n')) return 1;
                if (direction.Contains('s')) return -1;
                return 0;
            }
        }

    }
}
