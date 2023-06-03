namespace CI.Business.Entity
{
	public class UserInfo
	{
		public int UserID { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public DateTime UserAdded { get; set; }
		public bool IsActive { get; set; }
	}
}
