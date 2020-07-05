//const { debug } = require('console');

var io = require('socket.io')({
	transports: ['websocket'],
});

io.attach(4445);
let room = ['room1','room2','room3','room4','room5','room6','room7','room8','room9','room10'];
// var userlist = {
// 	users: [],
// 	createUser(id) {
// 		this.users.push({
// 		id
// 		})
// 	},
// 	deleteUser(id) {
// 		let idx = this.users.findIndex(x => x.id == id)
// 		if (idx != -1)
// 		this.users.splice(idx, 1)
// 	},
// 	has(id) {
// 		console.log(this.users.findIndex(x => x.id == id))
// 		return this.users.findIndex(x => x.id == id)
		
//     }
// }
io.on('connection', socket=>{
	socket.on("joinRoom",sid=> {
		id = sid['sid']
		key= sid["key"]
		// if (userlist.has(id) == -1 && id != '') {
		// 	userlist.createUser(id)
		// 	// 앞에서 둘씩 잘라서 room에 넣음
		// 	console.log(userlist.users[0])
		// 	console.log(userlist.users[1])
		// 	console.log(userlist.users[2])
		// 	console.log(userlist.users[3])
		// }
		//userlist.createUser(id)
		//console.log(userlist.users[0])
		//console.log(userlist.users[1])
		//console.log(userlist.users[2])
		//console.log(userlist.users[3])
		// 앞에서 둘씩 잘라서 room에 넣음
		socket.join(room[key], () => {
			console.log(' join a ' + room[key]);
			console.log(id)
			//io.to(room[key]).emit('joinRoom',{'key':key});
		  });
		// if(userlist.users.length()==2*key){
		// 	key++
		// }
		
		//  socket.emit("joinRoom",
		//  {
		// 	'sid':id,
		//  	'key':key
		//  })
	})

	// socket.on("boomRoom", data => {
	// io.sockets.to(data.roomCode).emit("boomRoom", data)

	// })
	// socket.on("getMsg", data => {
	// socket.broadcast.to(data.roomCode).emit("getMsg", data)
	// })

	// socket.on('disconnect', data => [
	// 	userlist.deleteUser(socket.id)
	// ])

	socket.on('disconnect', () => {
		console.log('user disconnected');
	  });
	
	
	socket.on('leaveRoom', key => {
		socket.leave(room[key], () => {
			console.log('leave a ' + room[key]);
			console.log(id)
			//io.to(room[key]).emit('leaveRoom', {'key':key});
		});
	});

	socket.on('Character',CharacterData=>{
		socket.emit('Character',CharacterData);
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

	socket.on('Mycard',data=>{
		console.log(data)
		socket.emit('Opponentcard',data)
	})

})



