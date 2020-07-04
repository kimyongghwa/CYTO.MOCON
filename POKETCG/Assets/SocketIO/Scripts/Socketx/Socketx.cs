using System.Collections;
using UnityEngine;
using SocketIO;
using System;
using System.Collections.Generic;


public class Socketx : MonoBehaviour
{
    private SocketIOComponent socket;

    public void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        socket.On("open", TestOpen);

        socket.On("boop", (SocketIOEvent e) => {
            Debug.Log(string.Format("[name: {0}, data: {1}]", e.name, e.data));
            data["email"] = "some@email.com";
            data["pass"] = "1234";
            socket.Emit("user:login", new JSONObject(data));
            Debug.Log(string.Format("[name: {0}, data: {1}]", e.name, e.data));
        });

        socket.On("receive", (SocketIOEvent e) => {
            Debug.Log(string.Format("[name: {0}, data: {1}]", e.name, e.data));
            data["email"] = "aa@email.com";
            data["pass"] = "aa";
            //socket.Emit("user:login", new JSONObject(data));
            Debug.Log(string.Format("[name: {0}, data: {1}]", e.name, e.data));
        });

        socket.On("error", TestError);
        socket.On("close", TestClose);

        StartCoroutine("BeepBoop");
    }

    private IEnumerator BeepBoop()
    {
        // wait 1 seconds and continue
        yield return new WaitForSeconds(1);

        socket.Emit("beep");

        // wait 3 seconds and continue
        yield return new WaitForSeconds(3);

        socket.Emit("beep");

        // wait 2 seconds and continue
        yield return new WaitForSeconds(2);

        socket.Emit("beep");

        // wait ONE FRAME and continue
        yield return null;

        socket.Emit("beep");
        socket.Emit("beep");
    }

    Dictionary<string, string> data = new Dictionary<string, string>();

    public void TestOpen(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }
    public void TestBoop(SocketIOEvent e)
    {
        data["email"] = "some@email.com";
        data["pass"] = "1234";
        socket.Emit("user:login", new JSONObject(data));
        Debug.Log(string.Format("[name: {0}, data: {1}]", e.name, e.data));

    }



    public void TestError(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
    }

    public void TestClose(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
    }
}