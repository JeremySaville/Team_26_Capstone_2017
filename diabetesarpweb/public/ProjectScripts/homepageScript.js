
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

function writeUserData() {
    var user = firebase.auth().currentUser;
    var uid = user.uid;
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

//function stepForwardToggle(){
	//document.getElementById('next').disabled = false;
//}

function stepForward() {
    
    window.location = "quizpage.html";
    
}

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
	//document.getElementById('submit').addEventListener('click', stepForward, false);
    
}


//function fillUid() {

//}

window.onload = function () {
    userDetails();
}
