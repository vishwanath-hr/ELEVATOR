'use restrict';

//signalR hub
function LoadHub() {
    elevator = $.connection.elevatorHub;

    elevator.client.addMessage = function (message) {
        //parse available json
        var elevator = jQuery.parseJSON(message);
        
        //clear td
        $("tbody").find('td.' + elevator.ElevatorId).html('');
        
        //Update totalpeople
        var span = "<span>" + elevator+ "</span";
        $("tbody").find("tr." + elevator.CurrentFloor).find('td.' + elevator.ElevatorId).html(elevator.TotalPeople);
        
        //update img
        var img = "<img src='content/images/"+ elevator.Direction +".gif' />";
        $("tbody").find("tr." + elevator.CurrentFloor).find('td.' + elevator.ElevatorId).append(img);
    };

    $.connection.hub.start({ transport: ['webSockets', 'serverSentEvents', 'longPolling'] });
}
