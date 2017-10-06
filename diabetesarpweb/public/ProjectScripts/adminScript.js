
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

function userDetails() {
    // Listening for auth state changes.
    // [START authstatelistener]
    firebase.auth().onAuthStateChanged(function (username) {

        if (username) {
            // User is signed in.
            //user = username;
            var displayName = username.displayName;
            var email = username.email;
            var emailVerified = username.emailVerified;
            var photoURL = username.photoURL;
            var isAnonymous = username.isAnonymous;
            var uid = username.uid;
            var providerData = username.providerData;
            //return firebase.database().ref('/users/' + uid).once('value').then(function (snapshot){
            //    var isAdmin = (snapshot.val().isAdmin);
            //});
            //if (isAdmin = true) {
            //    document.getElementById('admin').disabled = false;
            //};
            // [Fill textarea and input field from user details]
            
            //document.getElementById('uuidinput').defaultValue = uid;
            document.getElementById('quickstart-sign-in-status').textContent = 'Signed in';
            //document.getElementById('quickstart-account-details').textContent = JSON.stringify(user, null, '  ');
            document.getElementById('quickstart-account-details').textContent = email + ' ' + uid;
            
            
        } else {
            // User is signed out.
            document.getElementById('quickstart-sign-in-status').textContent = 'Signed out';
            document.getElementById('quickstart-account-details').textContent = 'null';
            window.alert("Please Login before viewing this page");
            window.location = 'index.html';
            
        }
        
    });
    // [END authstatelistener]
    //event listener for submitting data
    //document.getElementById('details').addEventListener('click', function () { window.location = 'detailsPage.html'; });
    //document.getElementById('dktquiz').addEventListener('click', function () { window.location = 'QuizPage.html'; });
    //document.getElementById('pedsql').addEventListener('click', function () { window.location = 'pedsQLPage.html'; });
    ////document.getElementById('admin').addEventListener('click', function () { window.location = ''; });
    
}

window.onload = function () {
    userDetails();
}
