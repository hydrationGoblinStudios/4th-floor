# amogus
 
public void BtnCreateAccount()
ShowScreen (Screens.Loading);
string username = usernameInput.text; string email = emailInput.text;
string password = password Input.text;
string _confirmPassword = confirm Password Input.text;
if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_confirmPassword))
Debug.Log("Por favor preencher todos os campos"); ShowMessage("Por favor preencher todos os campos"); ShowScreen (Screens.CreateAccount);
else if (_username. Length < 4)
{
Debug.Log("Nome de usuário precisa ter pelo menos 4 carcateres"); ShowMessage("Nome de usuário precisa ter pelo menos 4 carcateres"); ShowScreen (Screens. CreateAccount);
else if (_password. Length < 5)
{
ShowMessage("Senha precisa ter pelo menos 5 caracteres") ShowScreen (Screens.CreateAccount);
else if (_password != _confirmPassword)
{
else
Debug.Log("A senha não confere!!") ShowMessage("A senha não confere!!") ShowScreen (Screens.CreateAccount);
{
PlayfabManager.instance.CreateAccount(_username, email, _password);