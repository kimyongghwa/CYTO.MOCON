const { debug } = require('console');

var io = require('socket.io')({

	transports: ['websocket'],
	
	});
	io.attach(4445);
	var userlist = {

	users: [],
	createUser(id) {
	this.users.push({
	id
	})
	},
	deleteUser(id) {
	let idx = this.users.findIndex(x => x.id == id)
	if (idx != -1)
	this.users.splice(idx, 1)
	},
	}
	io.on('connection', socket=>{
	socket.on("joinRoom", data => {
	userlist.createUser(socket.id)
	//socket.join(data.roomCode) 앞에서 둘씩 잘라서 매칭
	socket.emit("joinRoom", data)
	})
	// socket.on("boomRoom", data => {
	// io.sockets.to(data.roomCode).emit("boomRoom", data)
	
	// })
	// socket.on("getMsg", data => {
	// socket.broadcast.to(data.roomCode).emit("getMsg", data)
	// })
	socket.on('disconnect', data => [
	userlist.deleteUser(socket.id)
	])
	socket.on('Character', function(){
	socket.emit('Character');
	});
	socket.on('Card', data=>{
	socket.emit('Card');
	});
	socket.on('Skill', data=>{
	socket.emit('Skill');
	});
	socket.on('beep',()=>{
		socket.emit('boop');
	});
	socket.on('user:login',data=>{
		console.log(data)
		socket.emit('receive',data)
	})
})