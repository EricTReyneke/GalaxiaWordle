function Validation(fullName, userName, email, password, confirmPassword) {

    // Check if any field is empty
    if (!fullName || !userName || !email || !password || !confirmPassword) {
        alert('All fields are required!');
        return false;
    }

    // Check if full name contains at least two words (assuming a first name and last name)
    if (fullName.split(' ').length < 2) {
        alert('Please enter your full name (first and last name).');
        return false;
    }

    // Check if email is valid (simple validation)
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    if (!emailPattern.test(email)) {
        alert('Please enter a valid email address.');
        return false;
    }

    // Check if password is at least 8 characters long
    if (password.length < 8) {
        alert('Password should be at least 8 characters long.');
        return false;
    }

    // Check if password and confirmPassword are the same
    if (password !== confirmPassword) {
        alert('Passwords do not match.');
        return false;
    }

    // If all validations pass
    return true;
}

function RegisterUser() {
    var fullName = document.getElementById('fullName').value;
    var userName = document.getElementById('UserName').value;
    var email = document.getElementById('email').value;
    var password = document.getElementById('Password').value;
    var confirmPassword = document.getElementById('confirmPassword').value;

    if (!Validation(fullName, userName, email, password, confirmPassword))
    {
        return;
    }

    var token = $('[name=__RequestVerificationToken]').val();

    var dataToSend = {
        FullName: fullName,
        UserName: userName,
        Email: email,
        Password: password,
        __RequestVerificationToken: token
    };

    var lekker = JSON.stringify(dataToSend);

    $.ajax({
        url: '/api/Registration/CreateNewUser',
        type: 'POST',
        contentType: 'application/json',
        data: lekker,
        success: function (response) {
            if (response) {
                console.log("User created successfully");
            } else {
                console.error("Failed to create user");
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error('Server error:', errorThrown);
        }
    });
}