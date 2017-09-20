
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

var database = firebase.database();

function writeDKTdata() {
    var user = firebase.auth().currentUser;
    var uid = user.uid;
    //var newPostRef = postListRef.push();
    var a1 = document.getElementsByName('optradio1').value;
    var a2 = document.getElementsByName('optradio2').value;
    firebase.database().ref('DKT/' + uid).set({
        Q1: a1
    });
}

function userDetails() {
    // Listening for auth state changes.
    // [START authstatelistener]
    firebase.auth().onAuthStateChanged(function (user) {

        if (user) {
            // User is signed in.
            var displayName = user.displayName;
            var email = user.email;
            var emailVerified = user.emailVerified;
            var photoURL = user.photoURL;
            var isAnonymous = user.isAnonymous;
            var uid = user.uid;
            var providerData = user.providerData;
            // [Fill textarea and input field from user details]

            //document.getElementById('uuidinput').defaultValue = uid;
            document.getElementById('quickstart-sign-in-status').textContent = 'Signed in';
            //document.getElementById('quickstart-account-details').textContent = JSON.stringify(user, null, '  ');
            document.getElementById('quickstart-account-details').textContent = email + ' ' + uid;

        } else {
            // User is signed out.
            document.getElementById('quickstart-sign-in-status').textContent = 'Signed out';
            document.getElementById('quickstart-account-details').textContent = 'null';
            window.alert("Please Login before completing your quiz");
            window.location = 'index.html'

        }

    });
    // [END authstatelistener]
    //event listener for submitting data
    document.getElementById('submit').addEventListener('click', writeDKTdata, false);
    //document.getElementById('submit').addEventListener('click', stepForward, false);

}

window.onload = function () {
    userDetails();
}
