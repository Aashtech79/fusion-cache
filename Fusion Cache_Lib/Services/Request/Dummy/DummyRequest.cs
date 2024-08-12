using Newtonsoft.Json;

namespace Fusion_Cache_Lib.Services.Request.Dummy
{
	public class Employee
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("employee_name")]
		public string EmployeeName { get; set; }

		[JsonProperty("employee_salary")]
		public decimal EmployeeSalary { get; set; }

		[JsonProperty("employee_age")]
		public int EmployeeAge { get; set; }

		[JsonProperty("profile_image")]
		public string ProfileImage { get; set; }
	}

	public class ApiResponse
	{
		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("data")]
		public List<Employee> Data { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }
	}
}
