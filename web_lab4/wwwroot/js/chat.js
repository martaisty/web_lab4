const allUsers = 'All users';
var currentUser = '';
var selectedUser = allUsers;
var users = [];

const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

hubConnection.on("ReceiveMessage", function (data) {
    addNewMessage(data);
});

hubConnection.on("Notify", function (data) {
    addNotification(data);
});

hubConnection.on("Users", function (data) {
    addUsers(['All users', ...data]);
});

function addNewMessage(body) {
    const author = body.username;
    const messageContent = `
        <div class="msg__username">${author}</div>
        <div class="msg_text">${body.text}</div>`;
    $('<div>').addClass('msg')
        .addClass(author === currentUser ? 'msg--my' : 'msg--other')
        .html(messageContent)
        .appendTo($('#messages'));
    document.getElementById('messages').scrollTop = document.getElementById('messages').scrollHeight;
}

function addNotification(body) {
    $('<div>').addClass('system-msg')
        .text(body.text)
        .appendTo($('#messages'));
    document.getElementById('messages').scrollTop = document.getElementById('messages').scrollHeight;
}

function sendMessage() {
    const message = $('#message-text').val();
    if (selectedUser === allUsers) {
        hubConnection.invoke("SendMessage", message);
    } else {
        hubConnection.invoke("SendPrivateMessage", selectedUser, message);
    }
}

function addUsers(users) {
    const inputs = users
        .filter(val => val !== currentUser)
        .map(val =>
            $('<div class="form-check">').html(
                `<label class="form-check-label">
                    <input
                        class="form-check-input"
                        type="radio"
                        name="active-username"
                        ${val === selectedUser ? "checked" : ""}/>
                    ${val}
                </label>`)
                .click(function (data) {
                    selectedUser = data.target.parentElement.innerText;
                })
        );
    const usersElement = $('#users');
    usersElement.empty();
    inputs.forEach(val => usersElement.append(val));
}

$(document).ready(function () {
    $('#new-msg-form').submit(function (event) {
        sendMessage();
        $('#message-text').val('');
        event.preventDefault();
    });
    
    currentUser = $('#username').val();

    hubConnection.start();
});
