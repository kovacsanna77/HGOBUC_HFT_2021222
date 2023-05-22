let networks = [];
let connection = null;
getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:27826/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("NetworkCreated", (user, message) => {
        getdata();
    });

    connection.on("NetworkDeleted", (user, message) => {
        getdata();
    });
    connection.on("NetworkUpdated", (user, message) => {
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
  
    await fetch('http://localhost:27826/Network')
        .then(x => x.json())
        .then(y => {
            networks = y;
           
           display();
        });
   
}

function display() {
   
    document.getElementById('resultarea').innerHTML = '';
    networks.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.networkId + "</td>"
            + "<td>" + t.networkName + "</td><td>"
            + `<button type="button" onclick="remove(${t.networkId})">Delete</button>`
            + "</td></tr>";
    });
}

function remove(id) {
    fetch('http://localhost:27826/Network/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            console.log(networks.length);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
   
}

function create() {
    let name = document.getElementById('networkname').value;
    fetch('http://localhost:27826/Network', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { networkName: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            console.log(networks.length);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}