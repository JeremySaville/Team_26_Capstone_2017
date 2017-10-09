
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

var postTime = new Date();

var radioAnswers = new Array();


$(document).ready(function () {
    $("button#submit").click(function () {

        $('input:radio').each(function () {
            if ($(this).is(':checked')) {
                var radio = { name: $(this).attr('name'), value: $(this).val() };
                radioAnswers.push(radio);
            }
        });

        console.log(JSON.stringify(radioAnswers));
        writeQuizData(radioAnswers);

    });
});


function writeQuizData(radioAnswers) {
    var user = firebase.auth().currentUser;
    var uid = user.uid;
    var myRef = firebase.database().ref().push();
    var key = myRef.key;
    firebase.database().ref('DKT/' + uid + '/' + key).set({
            created: firebase.database.ServerValue.TIMESTAMP,
            q01: radioAnswers[0],
            q02: radioAnswers[1],
            q03: radioAnswers[2],
            q04: radioAnswers[3],
            q05: radioAnswers[4],
            q06: radioAnswers[5],
            q07: radioAnswers[6],
            q08: radioAnswers[7],
            q09: radioAnswers[8],
            q10: radioAnswers[9],
            q11: radioAnswers[10],
            q12: radioAnswers[11],
            q13: radioAnswers[12],
            q14: radioAnswers[13],
            q15: radioAnswers[14],
            q16: radioAnswers[15],
            q17: radioAnswers[16],
            q18: radioAnswers[17],
            q19: radioAnswers[18],
            q20: radioAnswers[19],
            q21: radioAnswers[20],
            q22: radioAnswers[21],
            q23: radioAnswers[22]

    });
        console.log(key);
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
            window.location = 'index.html';

        }

    });
    // [END authstatelistener]
    //event listener for submitting data
    document.getElementById('back').addEventListener('click', function () { window.location = 'LandingPage.html'; });
    //document.getElementById('submit').addEventListener('click', writeQuizData, false);
    //document.getElementById('submit').addEventListener('click', stepForward, false);

}

window.onload = function () {
    userDetails();
}
