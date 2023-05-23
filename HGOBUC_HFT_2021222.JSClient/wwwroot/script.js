let stats = [];
let a10 = [];
let a5m = [];
let m10 = [];
let avgratingbynetwork = [];
let avgeppernetwork = [];

Actorswith10rated();
Actorswith5ratingandmovie();
Movieswith10rating();
AvgRatingByNetwork();
AvgEpisodesPerNetwork()

function Actorswith5ratingandmovie() {
    fetch('http://localhost:27826/Stat/ActorWith5RatedMovie')
        .then(x => x.json())
        .then(y => {
            a5m = y;
            console.log(a5m);
            displaya5m();
        }
        );

} function displaya5m() {
    a5m.forEach(t => {
        document.getElementById('actorswith5').innerHTML +=
            "<tr><td>" + t.key + "</td><td>" + t.value + "</td></tr>";
    })
}


function AvgRatingByNetwork() {
    fetch('http://localhost:27826/Stat/AvgMovieRateByNetwork')
        .then(x => x.json())
        .then(y => {
            avgratingbynetwork = y;
            console.log(avgratingbynetwork);
           displayavgratingbynetw();
        }
        );
}

function displayavgratingbynetw() {
    avgratingbynetwork.forEach(t => {
        document.getElementById('avgratingnetwork').innerHTML +=
            "<tr><td>" + t.key + "</td><td>" + t.value + "</td></tr>";
    })
}




function AvgEpisodesPerNetwork() {
    fetch('http://localhost:27826/Stat/AvgEpisodesPerNetwork')
        .then(x => x.json())
        .then(y => {
            avgeppernetwork = y;
            console.log(avgeppernetwork);
            displayAvgEp();
        }
        );

} function displayAvgEp() {
    avgeppernetwork.forEach(t => {
        document.getElementById('avgEpPerNetwork').innerHTML +=
            "<tr><td>" + t.key + "</td><td>" + t.value + "</td></tr>";
    })
}





function Movieswith10rating() {
    fetch('http://localhost:27826/Stat/MoviesWith10Rating')
        .then(x => x.json())
        .then(y => {
           m10 = y;
            console.log(m10);
            displaym10();
        }
        );
}
function displaym10() {
    m10.forEach(t => {
        document.getElementById('movies10').innerHTML +=
            "<tr><td>" + t.key + "</td><td>" + t.value + "</td></tr>";
    })

}


function displaya10() {
   
    a10.forEach(t => {
        document.getElementById('actorswith10').innerHTML +=
            "<tr><td>" + t + "</td></tr>"
            
    });
}

function Actorswith10rated() {
    fetch('http://localhost:27826/Stat/ActorsWith10Rating')
        .then(x => x.json())
        .then(y => {
            a10 = y;
            console.log(a10);
            displaya10();
}
    );
}