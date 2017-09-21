
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

var pubNumbers = 23
var test = new Array();
function getResults() {
    var numQuestions = document.getElementsById('optradio').length;
    var radios = document.getElementsById('optradio');
    for (i = 0; i < numQuestions; i++) {
        if (radios[i].checked) {
            return radios[i].value;

        }
    }
    return null;
}

function check() {
    for (var i = 1; i <= pubNumbers; i++) {
        //console.log(i,getCheckedValue('Q'+i));
        test[i - 1] = getResults('Q' + i);
    }
    //console.log(test);
    document.getElementById('quickstart-account-details').textContent = test;
}

//function getResults(form) {
//    var answers = 0;
//    var radios = form.elements["radio"];
//    for (var r = 0; r < radios.length; r++) {
//        if (radios[r].checked) {
//            answers = parseInt(radios[r].value);
//        }
//    }
//    return answers;
//    document.getElementById('quickstart-account-details').textContent = 'null';
//}

//function numQuestions(form) {
//    var radio = getResults(form);
//    form.elements["total"].value = radio;
//}

//function writeDKTdata() {
//    var user = firebase.auth().currentUser;
//    var uid = user.uid;
//    //var newPostRef = postListRef.push();
//    //var radioResults = 'Answers: ';
//    for (var i = 0; i < form.elements.length; i++) {
//        if (form.elements[i].type == 'radio') {
//            if (form.elementsi[i].checked == true) {
//                var radioResults = form.elements[i].value + ' ';
//                firebase.database().ref('DKT/' + uid).set({
//                    Quiz: radioResults
                    
//                });
//        }
//    }
//    //document.getElementById("radioResults").innerHTML = radioResults;
//    //firebase.database().ref('DKT/' + uid).set({
//    //    Q1: a1,
//    //    Q2: a2
//    //});
//}

//function writeDKTdata() {
//    var user = firebase.auth().currentUser;
//    var uid = user.uid;
//    //var newPostRef = postListRef.push();
//    var radioResults = 'Answers: ';
//    for (var i = 0; i < document.getElementsByName('optradio').length; i++) {
//        if (){
//            var a1 = document.getElementsByName('optradio1')[i].value;
//            //var a2 = document.getElementsByName('optradio2')[i].value;
//        }
//    }
//    firebase.database().ref('DKT/' + uid).set({
//        Q1: a1,
//        Q2: a2
//    });
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
    //document.getElementById('submit').addEventListener('click', writeDKTdata, false);
    document.getElementById('submit').addEventListener('click', check, false);
    //document.getElementById('submit').addEventListener('click', stepForward, false);

}

window.onload = function () {
    userDetails();
}
