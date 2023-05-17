namespace App.Model
{
    public struct StudentInfo
    {
        public string group;
        public int id;
        public int group_pos;
        public StudentInfo(string group, int id, int group_pos)
        {
            this.group = group;
            this.id = id;
            this.group_pos = group_pos;
        }
    }
}