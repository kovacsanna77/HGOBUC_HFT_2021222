let roles = [];
let connection = null;
getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:27826/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("RoleCreated", (user, message) => {
        getdata();
    });

    connection.on("RoleDeleted", (user, message) => {
        getdata();
    });
    connection.on("RoleUpdated", (user, message) => {
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
    await fetch('http://localhost:27826/Role')
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
            "<tr><td>" + t.roleId + "</td>"
        + "<td>" + t.roleName + "</td><td>"+ t.priority + "</td>"
        + "<td>" + t.actor.actorName + "</td><td>"
            + `<button type="button" onclick="remove(${t.roleId})">Delete</button>`
            + "</td></tr>";
    });
}

function remove(id) {
    fetch('http://localhost:27826/Role/' + id, {
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
    let name = document.getElementById('rolename').value;
    var _actor = { actorName: "-" };
    fetch('http://localhost:27826/Role', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { roleName: name, actor: _actor })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}