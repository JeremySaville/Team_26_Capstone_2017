
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



function userDetails() {
    firebase.auth().onAuthStateChanged(function (user) {
        //window.user = user;
        //var user = firebase.auth().currentUser;
        //var name, email, photoUrl, uid, emailVerified;

        if (user) {

            var name = user.displayName;
            var email = user.email;
            var photoUrl = user.photoUrl;
            var emailVerified = user.emailVerified;
            var uid = user.uid;
            document.getElementById('quickstart-account-details').textContent = JSON.stringify(user, null, '  ');
            document.getElementsByName('uuidinput')[0].placeholder = email;
        } else {
            document.getElementsByName('uuidinput')[0].placeholder = "No User Logged In";
            document.getElementById('quickstart-account-details').textContent = 'null';
        }
    }
        }

//function fillUid() {

//}

window.onload = function () {
    userDetails();
}
