let movies = [];
let connection = null;

let movieIdtoupdate = -1;
setupSignalR();
getdata();




function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:27826/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("MovieCreated", (user, message) => {
        getdata();
    });

    connection.on("MovieDeleted", (user, message) => {
        getdata();
    });
    connection.on("MovieUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:27826/Movie/')
        .then(x => x.json())
        .then(y => {
            movies = y;
            console.log(movies);
           display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = '';
    movies.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.movieId + "</td>"
            + "<td>" + t.title + "</td>"
            + "<td>" + t.aired + "</td>"
            + "<td>" + t.episodes + "</td>"
            + "<td>" + t.duration + " min" + "</td>"
            + "<td>" + t.rating + "</td>"
        + "<td>" + t.network.networkName + "</td><td>"
        + `<button type="button" onclick="remove(${t.movieId})">Delete</button>`
        + `<button type="button" onclick="ShowUpdate(${t.movieId})">Update</button>`
            + "</td></tr>";
    });
}
function ShowUpdate(id) {
    document.getElementById('movienametoUpdate').value = movies.find(t => t.movieId== id)['title'];
    document.getElementById('updateformdiv').style.display = 'inline';
    movieIdtoupdate = id;
}

function update() {
   
    let name = document.getElementById('movienametoUpdate').value;
    
    fetch('http://localhost:27826/Movie', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { title: name, movieId: movieIdtoupdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            console.log(movies);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
    document.getElementById('updateformdiv').style.display = 'none';
}


function remove(id) {
    fetch('http://localhost:27826/Movie/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let name = document.getElementById('moviename').value;
    var _movie = { networkName: "-" };
    fetch('http://localhost:27826/Movie/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { title: name, network: _movie })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            console.log(movies);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
    
}