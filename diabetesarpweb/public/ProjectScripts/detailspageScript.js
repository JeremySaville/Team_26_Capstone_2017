
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

// initialise DB
var database = firebase.database();

// references for randomisationtotals child entries
var Paper0 = firebase.database().ref('RandomisationTotals/Paper0');
var AppNormal1 = firebase.database().ref('RandomisationTotals/AppNormal1');
var AppGamified2 = firebase.database().ref('RandomisationTotals/AppGamified2');

// global variables for RandomisationTotals counts
var Paper0Count;
var AppNormal1Count;
var AppGamified2Count;

// fills count variables 
function getData() {
    Paper0.on('value', function (snapshot) {
        
        Paper0Count = snapshot.val();
        console.log('Randomisation totals for Paper0 = ' + Paper0Count);
        return Paper0Count;
    });

    AppNormal1.on('value', function (snapshot) {
        
        AppNormal1Count = snapshot.val();
        console.log('Randomisation totals for AppNormal1 = ' + AppNormal1Count);
        return AppNormal1Count;
    });

    AppGamified2.on('value', function (snapshot) {
        
        AppGamified2Count = snapshot.val();
        console.log('Randomisation totals for AppGamified2 = ' + AppGamified2Count);
        return AppGamified2Count;
    });
}


function updateDataPaper0(groupNum) {
    firebase.database().ref('RandomisationTotals/').update({
        Paper0: groupNum + 1
    });
    
}

// updates AppNormal1
function updateDataAppNormal1(groupNum) {
    firebase.database().ref('RandomisationTotals/').update({
        AppNormal1: groupNum + 1
    });

}

// updates AppGamified2
function updateDataAppGamified2(groupNum) {
    firebase.database().ref('RandomisationTotals/').update({
        AppGamified2: groupNum + 1
    });

}


// Finds a random number between 0 and 2 and checks the relevant count variable
// updates the relevant child in the RandomisationTotals and returns the selected
// random value to writeUserData()
function getGroup() {

    var validGroup = 0;
    while (validGroup == 0) {
        var randGroup = Math.floor(Math.random() * 3);
        console.log(randGroup);
        if (randGroup == 0 && Paper0Count <= 29) {
            updateDataPaper0(Paper0Count);
            validGroup = 1;
            return randGroup;
        } else if (randGroup == 1 && AppNormal1Count <= 29) {
            updateDataAppNormal1(AppNormal1Count);
            validGroup = 1;
            return randGroup;
        } else if (randGroup == 2 && AppGamified2Count <= 29) {
            updateDataAppGamified2(AppGamified2Count);
            validGroup = 1;
            return randGroup;
        } else if (Paper0Count >= 30 && AppNormal1Count >= 30 && AppGamified2Count >= 30) {
            validGroup = 1;
            window.alert('All Test Groups have been filled. Please manually adjust the Firebase Database as necessary.');
            return randGroup = null;
            //break;
        }
    };

}



// Gets user inputs from html fields, calls randomisation function and submits to DB
// overwrites previous inputs
function writeUserData() {
    var user = firebase.auth().currentUser;
    var uid = user.uid;
    var name = document.getElementById('InputName').value;
    var phone = document.getElementById('InputPhone').value;
    var dob = document.getElementById('InputDOB').value;
    var hba1c = document.getElementById('InputHBA1C').value;
    var group = getGroup();
    firebase.database().ref('users/' + uid).set({
        username: name,
        phone: phone,
        dob: dob,
        HBA1C: hba1c,
        gamified: group
    });

}


function userDetails() {
    // Listening for auth state changes.
    // [START authstatelistener]
    firebase.auth().onAuthStateChanged(function (username) {
        
        if (username) {
            // User is signed in.
            
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
            window.location = 'index.html';
            
        }
        
    });
    // [END authstatelistener]
    //event listener for submitting data
    document.getElementById('back').addEventListener('click', function () { window.location = 'LandingPage.html'; });
    document.getElementById('submit').addEventListener('click', writeUserData, false);
	
    
}




window.onload = function () {
    // fill firebase.auth() details
    userDetails();
    //get values in firebase db diabetesarp/RandomisationTotals
    getData();
    
}
