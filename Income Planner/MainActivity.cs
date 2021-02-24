using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Threading.Tasks;

namespace Income_Planner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //Inputs in Main view
        EditText incomePerHourInput;
        EditText hoursWeeklyInput;
        EditText taxRateInput;
        EditText savingsInput;

        // Views
        TextView annualWorkView;
        TextView annualIncomeView;
        TextView annualTaxView;
        TextView annualSavingsView;
        TextView annualSpendableView;

        Button calculateButton;
        RelativeLayout resultLayout;
        bool inputCalculated = false;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ConnectViews();
        }

        void ConnectViews()
        {
            incomePerHourInput = FindViewById<EditText>(Resource.Id.IncomePerHourText);
            hoursWeeklyInput = (EditText)FindViewById(Resource.Id.WorkHoursInWeekText);
            taxRateInput = (EditText)FindViewById(Resource.Id.TaxRateText);
            savingsInput = (EditText)FindViewById(Resource.Id.SavingsRateText);

            annualWorkView = (TextView)FindViewById(Resource.Id.WorkTimeValue);
            annualIncomeView = (TextView)FindViewById(Resource.Id.GrossIncomeValue);
            annualTaxView = (TextView)FindViewById(Resource.Id.AnnualTaxValue);
            annualSavingsView = (TextView)FindViewById(Resource.Id.AnnualSavingsValue);
            annualSpendableView = (TextView)FindViewById(Resource.Id.SpendableIncomeValue);

            calculateButton = (Button)FindViewById(Resource.Id.calculateButton);
            resultLayout = (RelativeLayout)FindViewById(Resource.Id.ResultLayout);

            calculateButton.Click += CalculateButton_Click;

        }

        private void CalculateButton_Click(object sender, System.EventArgs e)
        {
            try {

                if (inputCalculated)
                {
                    inputCalculated = false;
                    calculateButton.Text = "Calculate your plans";
                    ClearInput();
                    return;
                }
                // Take inputs from user
                double incomePerHour = double.Parse(incomePerHourInput.Text);
                double hoursPerWeek = double.Parse(hoursWeeklyInput.Text);
                double taxRate = double.Parse(taxRateInput.Text);
                double savingsRate = double.Parse(savingsInput.Text);

                // Calculate resulting information
                double annualWorkHours = hoursPerWeek * 52;
                double annualIncome = annualWorkHours * incomePerHour;
                double annualTax = annualIncome * (taxRate / 100);
                double annualSavings = annualIncome * (savingsRate / 100);
                double annualSpendable = annualIncome - annualTax - annualSavings;

                // Display results
                annualWorkView.Text = annualWorkHours.ToString("#,##") + "h";
                annualIncomeView.Text = annualIncome.ToString("#,##") + " USD";
                annualTaxView.Text = annualTax.ToString("#,##") + " USD";
                annualSavingsView.Text = annualSavings.ToString("#,##") + " USD";
                annualSpendableView.Text = annualSpendable.ToString("#,##") + " USD";
                resultLayout.Visibility = Android.Views.ViewStates.Visible;

                inputCalculated = true;
                calculateButton.Text = "Clear";
            }
            catch
            {
                
            }
            }

        void ClearInput()
        {
            incomePerHourInput.Text = "";
            taxRateInput.Text = "";
            savingsInput.Text = "";
            hoursWeeklyInput.Text = "";

            resultLayout.Visibility = Android.Views.ViewStates.Invisible;
        }
    }
}