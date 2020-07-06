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
		console.log(sid)
		socket.join(room[key], () => {
			console.log(id +' join ' + room[key]);
			//io.to(room[key]).emit('joinRoom',{'key':key}); room 전체
			//socket.broadcast.to(room[key]).emit('joinRoom',{'key':key}); room에 나 뺀 애들한테
			socket.emit('joinRoom',{'key':key})
			console.log({'key':key})
		});

		// if(userlist.users.length()==2*key){
		// 	key++
		// }
		
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
	
	
	socket.on('leaveRoom', keydata => {
		key=keydata['key']
		console.log(keydata)
		socket.leave(room[key], () => {
			console.log(id+' leave ' + room[key]);
			//io.to(room[key]).emit('leaveRoom', {'key':key});
		});
	});

	// socket.on('user:login',data=>{
	// 	console.log(data)
	// 	socket.emit('receive',data)
	// })

	socket.on('MyCard',data=>{
		console.log(data)
		key = data['key']
		socket.broadcast.to(room[key]).emit('OpponentCard',data) 
	})


	socket.on('MyCharacter',data=>{
		console.log(data)
		key = data['key']
		socket.broadcast.to(room[key]).emit('OpponentCharacter',data) 
	})
})



