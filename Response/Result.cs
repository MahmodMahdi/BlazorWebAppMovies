namespace BlazorWebAppMovies.Response
{
	public class Result
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
		public string? ErrorMessage { get; set; }

		public static Result Success(string message = "Operation completed successfult")
		{
			return new Result
			{
				IsSuccess = true,
				Message = message
			};
		}
		public static Result Failure(string errorMessage)
		{
			return new Result
			{
				IsSuccess = false,
				Message = errorMessage
			};
		}
	}
	public class Result<T> : Result
	{
		public T? Data { get; set; }
		public static Result<T> Success(T data,string message = "Operation completed successfult")
		{
			return new Result<T>
			{
				IsSuccess = true,
				Data = data,
				Message = message
			};
		}
		public static Result<T> Failure(string errorMessage)
		{
			return new Result<T>
			{
				IsSuccess = false,
				ErrorMessage = errorMessage
			};
		}

	}
}
