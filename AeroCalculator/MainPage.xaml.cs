namespace AeroCalculator;

public partial class MainPage : ContentPage
{
	string previousOperator;
	string previousValue;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnNumberClicked(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		Input.Text += button.Text;
	}

	private void OnACClicked(object sender, EventArgs e)
	{
		Input.Text = "";
		previousValue = "";
	}

	private void OnOperatorClicked(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		Input.Text += button.Text;
		previousOperator = button.Text;
	}

	private string GetLastOperand(string expression)
	{
        int lastOperatorIndex = expression.LastIndexOfAny(new[] { '+', '-', '*', '/' });

		if (lastOperatorIndex == -1)
		{
			return expression;
		}

		string lastOperand = expression.Substring(lastOperatorIndex + 1);

		return lastOperand;
	}

	private void OnCalculateClicked(object sender, EventArgs e)
	{
		try
		{
            string expression = Input.Text;
			string previousCalculation = previousOperator + previousValue;

            if (HasOperator(expression))
			{
                var dataTable = new System.Data.DataTable();

                var result = dataTable.Compute(expression, "");

				string lastOperand = GetLastOperand(expression);

				previousValue = lastOperand;

                Input.Text = $"{result}";
            }
            else
            {
				var dataTable = new System.Data.DataTable();

                string lastOperand = GetLastOperand(expression + previousCalculation);

                var result = dataTable.Compute(expression + "" + previousCalculation, "");

                Input.Text = $"{result}";
            }
        }
		catch (Exception)
		{
			Input.Text = "Invalid expression";
		}
	}

	private bool HasOperator(string expression)
	{
		return expression.Contains("+")
			|| expression.Contains("-")
			|| expression.Contains("*")
			|| expression.Contains("/");

	}

}

