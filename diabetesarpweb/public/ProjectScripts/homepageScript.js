
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

function writeUserData(uid, username, name, phone, dob, hba1c) {
    var uid = document.getElementById('uuidinput').value;
    var name = document.getElementById('InputName').value;
    var phone = document.getElementById('InputPhone').value;
    var dob = document.getElementById('InputDOB').value;
    var hba1c = document.getElementById('InputHBA1C').value;
    firebase.database().ref('users/' + uid).set({
        username: name,
        phone: phone,
        dob: dob,
        HBA1C: hba1c
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
            
            document.getElementById('uuidinput').defaultValue = uid;
            document.getElementById('quickstart-sign-in-status').textContent = 'Signed in';
            //document.getElementById('quickstart-account-details').textContent = JSON.stringify(user, null, '  ');
            document.getElementById('quickstart-account-details').textContent = email + ' ' + uid;
            
        } else {
            // User is signed out.
            document.getElementById('quickstart-sign-in-status').textContent = 'Signed out';
            document.getElementById('quickstart-account-details').textContent = 'null';
            window.alert("Please Login before entering your details");
            window.location = 'index.html'
            
        }
        
    });
    // [END authstatelistener]
    //event listener for submitting data
    document.getElementById('submit').addEventListener('click', writeUserData, false);
    
}


//function fillUid() {

//}

window.onload = function () {
    userDetails();
}
