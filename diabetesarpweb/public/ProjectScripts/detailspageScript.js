
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
//var Paper0 = firebase.database().ref('RandomisationTotals/Paper0');
//var AppNormal1 = firebase.database().ref('RandomisationTotals/AppNormal1');
//var AppGamified2 = firebase.database().ref('RandomisationTotals/AppGamified2');

//function getData(randGroup) {
//    //var Paper = firebase.database().ref('RandomisationTotals/0Paper');
//    if (randGroup == 0) {
//        Paper0.on('value', function (snapshot) {
//            console.log(snapshot.val());
//            var Paper0 = snapshot.val();
//            return Paper0;
//        });
//    } else if (randGroup == 1){
//        AppNormal1.on('value', function (snapshot) {
//            console.log(snapshot.val());
//            var AppNormal1 = snapshot.val();
//            return AppNormal1;
//        });
//    } else if (randGroup == 2) {
//        AppGamified2.on('value', function (snapshot) {
//            console.log(snapshot.val());
//            var AppGamified2 = snapshot.val();
//            return AppGamified2;
//        });
//    }
//}

function getData(randGroup) {
    
    var Paper0 = firebase.database().ref('RandomisationTotals/Paper0');
    var AppNormal1 = firebase.database().ref('RandomisationTotals/AppNormal1');
    var AppGamified2 = firebase.database().ref('RandomisationTotals/AppGamified2');
    var groupCount;
    if (randGroup == 0) {
        //console.log('Paper');
        Paper0.once('value').then(function (snapshot) {
            console.log('First result ' + snapshot.val());
            groupCount = (snapshot.val() && snapshot.val().Paper0);
            console.log('Final Group Count ' + groupCount);
        });
        //console.log('Final Group Count ' + groupCount);
    } else if (randGroup == 1) {
        //console.log('AppNormal');
        AppNormal1.once('value').then(function (snapshot) {
            console.log('Second Result ' + snapshot.val());
            groupCount = (snapshot.val() && snapshot.val().AppNormal1);
            console.log('Final Group Count ' + groupCount);
        });
        //console.log('Final Group Count ' + groupCount);

    } else if (randGroup == 2) {
        //console.log('AppGamified');
        AppGamified2.once('value').then(function (snapshot) {
            console.log('Third Result ' + snapshot.val());
            groupCount = (snapshot.val() && snapshot.val().AppGamified2);
            console.log('Final Group Count ' + groupCount);
        });
        //console.log('Final Group Count ' + groupCount);
    }
    //return groupCount;
    //console.log('Final Group Count ' + groupCount);
    return 5;
}

function updateDataPaper0(groupNum) {
    firebase.database().ref('RandomisationTotals/').update({
        Paper0: groupNum + 1
    });
    
}

function updateDataAppNormal1(groupNum) {
    firebase.database().ref('RandomisationTotals/').update({
        AppNormal1: groupNum + 1
    });

}

function updateDataAppGamified2(groupNum) {
    firebase.database().ref('RandomisationTotals/').update({
        AppGamified2: groupNum + 1
    });

}

//var starCountRef = firebase.database().ref('posts/' + postId + '/starCount');
//starCountRef.on('value', function (snapshot) {
//    updateStarCount(postElement, snapshot.val());
//});

function getGroup() {

    var validGroup = 0;
    while (validGroup == 0) {
        var randGroup = Math.floor(Math.random() * 3);
        console.log(randGroup);
        if (getData(randGroup) <= 30) {
            
            if (randGroup == 0) {
                updateDataPaper0(getData(randGroup));
            } else if (randGroup == 1) {
                updateDataAppNormal1(getData(randGroup));
            } else if (randGroup == 2) {
                updateDataAppGamified2(getData(randGroup));
            }
            validGroup = 1;
        }
    };

    return randGroup;

}

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
        AppGroup: group
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
            window.location = 'index.html';
            
        }
        
    });
    // [END authstatelistener]
    //event listener for submitting data
    document.getElementById('back').addEventListener('click', function () { window.location = 'LandingPage.html'; });
    document.getElementById('submit').addEventListener('click', writeUserData, false);
	//document.getElementById('submit').addEventListener('click', stepForward, false);
    
}


//function fillUid() {

//}

window.onload = function () {
    userDetails();
    //getGroup();
    //console.log('Getting Data');
    //getData();
    //console.log('updating Data');
    //updateData();
    //console.log('Getting Data');
    //getData();
}
