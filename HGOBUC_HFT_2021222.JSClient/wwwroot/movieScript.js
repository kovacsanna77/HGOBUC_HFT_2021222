let movies = [];
let connection = null;
getdata();

setupSignalR();


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
            + "</td></tr>";
    });
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
    var _movie = {networkName: "-"}
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