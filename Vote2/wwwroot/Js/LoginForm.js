const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('containerLogIn');


signUpButton.addEventListener('click', () => {
    container.classList.add("right-panel-active");
});


signInButton.addEventListener('click', () => {
    container.classList.remove("right-panel-active");
});



var SignUp = function () {
    debugger
    var _SignUpForm = $("#SignUpForm").serialize();
    $("#BtnSignUp").html("Please Wait");
    $('#BtnSignUp').attr('disabled', 'disabled');
    $.ajax({
        type: "POST",
        url: "/LogIn/SignUp",
        data: _SignUpForm,
        success: function (result) {
            debugger
            if (result.IsSuccess) {
                debugger
                var EmailInput = $("#SignUpEmail").val();
                $("#signIn").click();
                $("#SignInEmail").val(EmailInput);
                $("#SignInPassword").focus();

            }
            else {
                Swal.fire({
                    title: result.Message,
                    icon: "warning"
                }).then(function () {
                    $("#BtnSignUp").html("Sign Up");
                    $('#BtnSignUp').removeAttr('disabled');
                });
            }
            
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}

var SignIn = function () {
    debugger
    var EmailLogIn = $("#SignInEmail").val();
    var PasswordLogIn = $("#SignInPassword").val();
    $("#BtnSignIn").html("Please Wait");
    $('#BtnSignIn').attr('disabled', 'disabled');
    $.ajax({
        type: "POST",
        url: "/LogIn/SignIn",
        data: {
            Email: EmailLogIn,
            Password: PasswordLogIn
        },
        success: function (result) {
            debugger
            if (result.IsSuccess) {
                location.href = "/Home/Index";

            }
            else {
                Swal.fire({
                    title: result.Message,
                    icon: "warning"
                }).then(function () {
                    $("#BtnSignIn").html("Sign In");
                    $('#BtnSignIn').removeAttr('disabled');
                });
            }
            
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}