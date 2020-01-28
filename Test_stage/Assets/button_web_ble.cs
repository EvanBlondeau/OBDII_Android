using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using Newtonsoft.Json; // on the top of the file.



public class button_web_ble : MonoBehaviour
{
    UnityEngine.TouchScreenKeyboard keyboard;
    TextMesh textObject;
    static string d = "JSON";
    static string f = "Pression athmosphérique";
    bool _threadRunning;
    Thread _thread;
    public String variable_ip;
    
    TextMesh textObject2;
    TextMesh textJson;
    GameObject gameObject;
    //Speed speed;

    // Start is called before the first frame update

    void Start()
    {
        textObject = GameObject.Find("JSON_Request").GetComponent<TextMesh>();
        textObject2 = GameObject.Find("IP_entrer_keyboard").GetComponent<TextMesh>();
        textJson = GameObject.Find("TextJSON").GetComponent<TextMesh>();
        speed = 0;
        rpm = 0;
    }

    void Update()
    {
        if (TouchScreenKeyboard.visible == false && keyboard != null)
        {
            if (keyboard.done == true)
            {
                if (keyboard.text != "")
                {
                    textObject2.text = "IP entrer : "+keyboard.text;
                    variable_ip = keyboard.text;
                    keyboard = null;
                    theardHttp();
                }
                else
                {
                    theardHttp();
                }
            }
        }
        textObject.text = d;
        textJson.text = f;
    }

    public void Input_new_ip()
    {
        // Single-line textbox with title
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "Single-line title");
    }

    public void theardHttp()
    {
        Debug.Log(variable_ip);

        _thread = new Thread(httpRequestThread);
        _thread.Start();
    }

    [HideInInspector] public float speed = 0;
    [HideInInspector] public float rpm = 0;
    private void httpRequestThread()
    {
        _threadRunning = true;
        bool workDone = false;
        // This pattern lets us interrupt the work at a safe point if neeeded.
        while (_threadRunning && !workDone)
        {
            Debug.Log("Making API Call...");
            d = Get("http://192.168.43."+variable_ip+":5000/");
            Debug.Log(d);
            PID stuff = JsonConvert.DeserializeObject<PID>(d.ToString());
            Debug.Log(stuff);
            f = (stuff.Pressure).ToString();
           // Debug.Log(stuff.Speed);
            speed = stuff.Speed;
           // Debug.Log(stuff.RPM);
            rpm = stuff.RPM;
           // Debug.Log(rpm);
            this.rpm = rpm;
            this.speed = speed;
            Thread.Sleep(200);
        }
        _threadRunning = false;
    }

    void OnDisable()
    {
        // If the thread is still running, we should shut it down,
        // otherwise it can prevent the game from exiting correctly.
        if (_threadRunning)
        {
            // This forces the while loop in the ThreadedWork function to abort.
            _threadRunning = false;

            // This waits until the thread exits,
            // ensuring any cleanup we do after this is safe. 
            _thread.Join();
        }

        // Thread is guaranteed no longer running. Do other cleanup tasks.
    }

    public void WebSocket()
    {
            using (var ws = new WebSocket(url: "ws://192.168.43."+variable_ip+":5000/live", onMessage: OnMessage, onError: OnError))
            {
                ws.Connect().Wait();
                Debug.Log("connected");
                ws.Send("Hello Server").Wait();

                Thread.Sleep(3000);
                Debug.Log("second");
                ws.Send("Hello Server").Wait();
                Thread.Sleep(3000);
            // Console.ReadKey(true);
            }

    }


    public static string Get(string uri)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }


    private static Task OnError(WebSocketSharp.ErrorEventArgs errorEventArgs)
    {
        Debug.Log("Error: {0}"+ errorEventArgs.Message +" Exception: {1}"+errorEventArgs.Exception);
        return Task.FromResult(0);
    }

    private static Task OnMessage(MessageEventArgs messageEventArgs)
    {
        Debug.Log("Message received: {0}"+ messageEventArgs.Text.ReadToEnd());
        return Task.FromResult(0);
    }


}

public class PID
{
    public long RPM { get; set; }
    public long Speed { get; set; }
    public long Pressure { get; set; }
}
