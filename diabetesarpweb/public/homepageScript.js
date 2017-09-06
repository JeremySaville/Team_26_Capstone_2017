
// Initialize Firebase
var config = {
    apiKey: "AIzaSyBWvmWgYk6iXjMAUryQDNfXHxiWjQHYrvE",
    authDomain: "diabetesarp.firebaseapp.com",
    databaseURL: "https://diabetesarp.firebaseio.com",
    projectId: "diabetesarp",
    storageBucket: "diabetesarp.appspot.com",
    messagingSenderId: "411612735580"
};
firebase.initializeApp(config);



function userDetails_Old() {
    //firebase.auth().onAuthStateChanged(function (user) {
        //window.user = user;
        //var user = firebase.auth().currentUser;
        //var name, email, photoUrl, uid, emailVerified;

        if (user) {

            var name = user.displayName;
            var email = user.email;
            var photoUrl = user.photoUrl;
            var emailVerified = user.emailVerified;
            var uid = user.uid;
           // document.getElementById('quickstart-account-details').textContent = JSON.stringify(user, null, '  ');
            document.getElementsByName('uuidinput')[0].placeholder = "User logged in";
        } else {
            document.getElementsByName('uuidinput')[0].placeholder = "No User Logged In";
            document.getElementById('quickstart-account-details').textContent = 'null';
        }
}

function userDetails() {
    // Listening for auth state changes.
    // [START authstatelistener]
    firebase.auth().onAuthStateChanged(function (user) {
        // [START_EXCLUDE silent]
        //document.getElementById('quickstart-verify-email').disabled = true;
        //document.getElementById('quickstart-verify-email').style.display = "none";

        // [END_EXCLUDE]
        if (user) {
            // User is signed in.
            var displayName = user.displayName;
            var email = user.email;
            var emailVerified = user.emailVerified;
            var photoURL = user.photoURL;
            var isAnonymous = user.isAnonymous;
            var uid = user.uid;
            var providerData = user.providerData;
            // [START_EXCLUDE]
            //document.getElementById('next').disabled = false;
            document.getElementsByName('uuidinput')[0].placeholder = uid;
            document.getElementById('quickstart-sign-in-status').textContent = 'Signed in';
            //document.getElementById('quickstart-sign-in').textContent = 'Sign out';
            //document.getElementById('quickstart-sign-in').disabled = false;
            //document.getElementById('quickstart-account-details').textContent = JSON.stringify(user, null, '  ');
            document.getElementById('quickstart-account-details').textContent = email + ' ' + uid;
            //if (!emailVerified) {
            //    document.getElementById('quickstart-verify-email').disabled = false;
            //}
            // [END_EXCLUDE]
            //window.location = 'homepage.html'
        } else {
            // User is signed out.
            // [START_EXCLUDE]
            document.getElementById('quickstart-sign-in-status').textContent = 'Signed out';
            //document.getElementById('quickstart-sign-in').textContent = 'Sign in';
            //document.getElementById('quickstart-sign-in').disabled = true;;
            document.getElementById('quickstart-account-details').textContent = 'null';
            //document.getElementById('next').disabled = true;
            // [END_EXCLUDE]
        }
        // [START_EXCLUDE silent]
        //document.getElementById('quickstart-sign-in').disabled = false;
        // [END_EXCLUDE]
    });
    // [END authstatelistener]
    //document.getElementById('quickstart-sign-in').addEventListener('click', toggleSignIn, false);
    //document.getElementById('quickstart-sign-up').addEventListener('click', handleSignUp, false);
    //document.getElementById('next').addEventListener('click', stepForward, false);
    //document.getElementById('quickstart-verify-email').addEventListener('click', sendEmailVerification, false);
    //document.getElementById('quickstart-password-reset').addEventListener('click', sendPasswordReset, false);
}


//function fillUid() {

//}

window.onload = function () {
    userDetails();
}
