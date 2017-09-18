
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

//document.getElementById('submit').addEventListener('click', submitQuiz, false);

//function submitQuiz() {

//}
//var answerArray = new Array();
//function check() {
//    var answers = document.forms[0];
//    var i;
//    for (i = 0; i < answers.length; i++) {
//        if (answers[i].checked) {
//            answerArray.push(answerArray[i].value)
//        }
//    }
//}
////var numQuestions = 22;
//var numQuestions = document.getElementById('optradio');
//var test = new Array();
//// following two functions only return a single value repeated in the array
//function getResults(quizForm1) {
//    var radios = document.getElementById('quizForm1');
//    for (i = 0; i < radios.length; i++) {
//        if (radios[i].checked) {
//            return radios[i].value;

//        }
//    }
//    return null;
//}

//function check() {
//    for (var i = 0; i <= numQuestions; i++) {
//        //console.log(i,getCheckedValue('Q'+i));
//        test[i] = getResults('optradio' + i);
//    }
//    //console.log(test);
//    document.getElementById('quickstart-account-details').textContent = test;
//}

//var pubNumbers = 23
//var test = new Array();
//function getResults() {
//    var numQuestions = document.getElementsById('optradio').length;
//    var radios = document.getElementsById('optradio');
//    for (i = 0; i < numQuestions; i++) {
//        if (radios[i].checked) {
//            return radios[i].value;

//        }
//    }
//    return null;
//}

//function check() {
//    for (var i = 1; i <= pubNumbers; i++) {
//        //console.log(i,getCheckedValue('Q'+i));
//        test[i - 1] = getResults('Q' + i);
//    }
//    //console.log(test);
//    document.getElementById('quickstart-account-details').textContent = test;
//}

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
    document.getElementById('submit').addEventListener('click', check, false);
    //document.getElementById('submit').addEventListener('click', stepForward, false);

}

window.onload = function () {
    userDetails();
}
