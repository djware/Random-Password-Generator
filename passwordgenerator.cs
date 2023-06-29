using System;
using System.Windows.Forms;

class RandomPasswordGenerator
{
    private const string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
    private const string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Digits = "0123456789";
    private const string SpecialCharacters = "!@#$%^&*()_+=-{}[]\\|:;\"'<>,.?/";

    private static Random rng = new Random();

    public static string GeneratePassword(int length, bool includeLowercase, bool includeUppercase, bool includeDigits, bool includeSpecialCharacters)
    {
        string validCharacters = string.Empty;

        if (!includeLowercase && !includeUppercase && !includeDigits && !includeSpecialCharacters)
        {
            throw new ArgumentException("At least one character type must be selected.");
        }

        if (includeLowercase)
            validCharacters += LowercaseLetters;

        if (includeUppercase)
            validCharacters += UppercaseLetters;

        if (includeDigits)
            validCharacters += Digits;

        if (includeSpecialCharacters)
            validCharacters += SpecialCharacters;

        char[] password = new char[length];

        for (int i = 0; i < length; i++)
        {
            int index = rng.Next(0, validCharacters.Length);
            password[i] = validCharacters[index];
        }

        return new string(password);
    }
}

class MainForm : Form
{
    private TextBox passwordTextBox;
    private Button generateButton;
    private Button copyButton;
    private Button exitButton;
    private NumericUpDown lengthNumericUpDown;
    private CheckBox lowercaseCheckBox;
    private CheckBox uppercaseCheckBox;
    private CheckBox digitsCheckBox;
    private CheckBox specialCharactersCheckBox;

    public MainForm()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        this.Text = "Random Password Generator";

        passwordTextBox = new TextBox()
        {
            Location = new System.Drawing.Point(20, 20),
            Size = new System.Drawing.Size(200, 20),
            ReadOnly = true
        };

        generateButton = new Button()
        {
            Location = new System.Drawing.Point(20, 160),
            Size = new System.Drawing.Size(130, 30),
            Text = "Generate Password"
        };
        generateButton.Click += GenerateButton_Click;

        copyButton = new Button()
        {
            Location = new System.Drawing.Point(160, 160),
            Size = new System.Drawing.Size(120, 30),
            Text = "Copy to Clipboard"
        };
        copyButton.Click += CopyButton_Click;

        exitButton = new Button()
        {
            Location = new System.Drawing.Point(160, 210),
            Size = new System.Drawing.Size(120, 30),
            Text = "Exit"
        };
        exitButton.Click += ExitButton_Click;

        lengthNumericUpDown = new NumericUpDown()
        {
            Location = new System.Drawing.Point(20, 50),
            Size = new System.Drawing.Size(100, 20),
            Minimum = 1,
            Maximum = 100,
            Value = 10
        };

        lowercaseCheckBox = new CheckBox()
        {
            Location = new System.Drawing.Point(20, 90),
            Size = new System.Drawing.Size(90, 20),
            Text = "Lowercase",
            Checked = true
        };

        uppercaseCheckBox = new CheckBox()
        {
            Location = new System.Drawing.Point(20, 120),
            Size = new System.Drawing.Size(90, 20),
            Text = "Uppercase",
            Checked = true
        };

        digitsCheckBox = new CheckBox()
        {
            Location = new System.Drawing.Point(160, 90),
            Size = new System.Drawing.Size(60, 20),
            Text = "Digits",
            Checked = true
        };

        specialCharactersCheckBox = new CheckBox()
        {
            Location = new System.Drawing.Point(160, 120),
            Size = new System.Drawing.Size(130, 20),
            Text = "Special Characters",
            Checked = true
        };

        this.Controls.Add(passwordTextBox);
        this.Controls.Add(generateButton);
        this.Controls.Add(copyButton);
        this.Controls.Add(exitButton);
        this.Controls.Add(lengthNumericUpDown);
        this.Controls.Add(lowercaseCheckBox);
        this.Controls.Add(uppercaseCheckBox);
        this.Controls.Add(digitsCheckBox);
        this.Controls.Add(specialCharactersCheckBox);
    }

    private void GenerateButton_Click(object sender, EventArgs e)
    {
        int passwordLength = (int)lengthNumericUpDown.Value;
        bool includeLowercase = lowercaseCheckBox.Checked;
        bool includeUppercase = uppercaseCheckBox.Checked;
        bool includeDigits = digitsCheckBox.Checked;
        bool includeSpecialCharacters = specialCharactersCheckBox.Checked;

        try
        {
            string password = RandomPasswordGenerator.GeneratePassword(passwordLength, includeLowercase, includeUppercase, includeDigits, includeSpecialCharacters);
            passwordTextBox.Text = password;
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CopyButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(passwordTextBox.Text))
        {
            Clipboard.SetText(passwordTextBox.Text);
            MessageBox.Show("Password copied to clipboard!");
        }
        else
        {
            MessageBox.Show("No password generated to copy!");
        }
    }

    private void ExitButton_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}

class Program
{
    [STAThread]
    static void Main()
    {
        Application.Run(new MainForm());
    }
}
