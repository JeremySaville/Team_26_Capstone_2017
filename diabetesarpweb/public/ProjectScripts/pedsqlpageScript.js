
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

var pedsqlAnswers = new Array();

$(document).ready(function () {
    $("button#submit").click(function () {

        $('input:radio').each(function () {
            if ($(this).is(':checked')) {
                var radio = { name: $(this).attr('name'), value: $(this).val() };
                pedsqlAnswers.push(radio);
            }
        });

        console.log(JSON.stringify(pedsqlAnswers));
        writepedsqlData(pedsqlAnswers);

    });
});

// working (but somewhat hardcoded) inputting quiz results from radio buttons
// will need to look into ways to make this tidier
function writepedsqlData(pedsqlAnswers) {
    var user = firebase.auth().currentUser;
    var uid = user.uid;
    firebase.database().ref('pedsQL/' + uid).set({
        q101: pedsqlAnswers[0],
        q102: pedsqlAnswers[1],
        q103: pedsqlAnswers[2],
        q104: pedsqlAnswers[3],
        q105: pedsqlAnswers[4],
        q106: pedsqlAnswers[5],
        q107: pedsqlAnswers[6],
        q108: pedsqlAnswers[7],
        q109: pedsqlAnswers[8],
        q110: pedsqlAnswers[9],
        q111: pedsqlAnswers[10],
        q112: pedsqlAnswers[11],
        q113: pedsqlAnswers[12],
        q114: pedsqlAnswers[13],
        q115: pedsqlAnswers[14],
        q201: pedsqlAnswers[15],
        q202: pedsqlAnswers[16],
        q203: pedsqlAnswers[17],
        q204: pedsqlAnswers[18],
        q205: pedsqlAnswers[19],
        q301: pedsqlAnswers[20],
        q302: pedsqlAnswers[21],
        q303: pedsqlAnswers[22],
        q304: pedsqlAnswers[23],
        q305: pedsqlAnswers[24],
        q306: pedsqlAnswers[25],
        q401: pedsqlAnswers[26],
        q402: pedsqlAnswers[27],
        q403: pedsqlAnswers[28],
        q501: pedsqlAnswers[29],
        q502: pedsqlAnswers[30],
        q503: pedsqlAnswers[31],
        q504: pedsqlAnswers[32]
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
    document.getElementById('back').addEventListener('click', function () { window.location = 'LandingPage.html'; });
    

}

window.onload = function () {
    userDetails();
}
