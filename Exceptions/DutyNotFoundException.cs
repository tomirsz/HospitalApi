using System;
    [Serializable]
    public class DutyNotFoundException : Exception
    {
        public DutyNotFoundException()
        {
        }

    public DutyNotFoundException(long id) : base(String.Format("Duty {} not exists", id))
    {
    }
}
