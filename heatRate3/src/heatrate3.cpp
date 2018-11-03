#include <dali-toolkit/dali-toolkit.h>
#include <sensor.h>
#include <stdbool.h>
#include <stdio.h>
#include <http.h>
#include <curl/curl.h>
#include <net_connection.h>
#include <dlog.h>

#define TAG "heartRate"

using namespace Dali;
using namespace Dali::Toolkit;


 http_session_h session = NULL;
 http_transaction_h transaction = NULL;

 // dlogutil

// This example shows how to create and display Hello World using a simple TextLabel
//
class HelloWorldController : public ConnectionTracker
{
public:
  TextLabel mText;

  static HelloWorldController* INSTNACE;
  static bool SEND_REQUEST;
  static sensor_type_e SENSOR_TYPE;

  HelloWorldController( Application& application )
  : mApplication( application )
  {
    // Connect to the Application's init signal
    mApplication.InitSignal().Connect( this, &HelloWorldController::Create );
  }

  ~HelloWorldController()
  {
    // Nothing to do here
  }

  static bool SendInfoCurl()

  {

  }

  static void header_cb(http_transaction_h transaction, char* header, size_t header_len, void* user_data)
  {
	  dlog_print(DLOG_DEBUG, TAG, "HEADER GOTTEN");
  }

  static void body_cb(http_transaction_h transaction, char* body, size_t size, size_t nmemb, void* user_data)
  {
	  dlog_print(DLOG_DEBUG, TAG, "BODY GOTTEN");
  }

  static void completed_cb(http_transaction_h transaction, void* user_data)
  {
	  dlog_print(DLOG_DEBUG, TAG, "TRANS COMPLETE");
	  SEND_REQUEST = true;
  }

  static void aborted_cb(http_transaction_h transaction, http_error_code_e error, void* user_data)
  {
	  dlog_print(DLOG_DEBUG, TAG, "ABORTED: %d", error);
	  SEND_REQUEST = true;
  }

  static bool SendInfo(std::string value)
  {
 	 int ret = HTTP_ERROR_NONE;

 	 ret = http_session_create(HTTP_SESSION_MODE_NORMAL, &session);
 	 if (ret != HTTP_ERROR_NONE)
 		dlog_print(DLOG_DEBUG, TAG, "http_session_create failed: %d", ret);

 	 ret = http_session_open_transaction(session, HTTP_METHOD_GET, &transaction);
 	 if (ret != HTTP_ERROR_NONE)
  		dlog_print(DLOG_DEBUG, TAG, "http_session_open_transaction failed: %d", ret);

 	/* Request URI for HTTP GET */
 	 std::string url = "https://140.140.140.30:45455/api/watch/setValue/" + value;


 	 /*
 	    Request URI for HTTPS GET:
 	    char uri[1024] = "https://httpbin.org/get";
 	    Request URI and data for HTTP POST:
 	    char uri[1024] = "http://posttestserver.com/post.php";
 	    const char* post_msg = "name=tizen&project=http";
 	 */

 	 ret = http_session_create(HTTP_SESSION_MODE_NORMAL, &session);
 	 if (ret != HTTP_ERROR_NONE)
		dlog_print(DLOG_DEBUG, TAG, "http_session_create failed: %d", ret);

 	 /* Transaction for HTTP and HTTPS GET */
 	 ret = http_session_open_transaction(session, HTTP_METHOD_GET, &transaction);
 	 if (ret != HTTP_ERROR_NONE)
 		dlog_print(DLOG_DEBUG, TAG, "http_session_open_transaction failed: %d", ret);

 	 ret = http_transaction_set_server_certificate_verification(transaction, false);
 	 if (ret != HTTP_ERROR_NONE)
		dlog_print(DLOG_DEBUG, TAG, "cert version failed: %d", ret);

 	 ret = http_transaction_request_set_uri(transaction, url.c_str());
 	 if (ret != HTTP_ERROR_NONE)
		dlog_print(DLOG_DEBUG, TAG, "http_transaction_request_set_uri failed: %d", ret);

 	 /*
 	    Data management for HTTP POST:
 	    http_transaction_set_ready_to_write(transaction, TRUE);
 	    http_transaction_request_write_body(transaction, post_msg);
 	 */


 	http_transaction_set_received_header_cb(transaction, header_cb, NULL);
 	http_transaction_set_received_body_cb(transaction, body_cb, NULL);
 	http_transaction_set_completed_cb(transaction, completed_cb, NULL);
 	http_transaction_set_aborted_cb(transaction, aborted_cb, NULL);

 	dlog_print(DLOG_DEBUG, TAG, "SENDING TO %s", url.c_str());
 	ret = http_transaction_submit(transaction);
 	if (ret != HTTP_ERROR_NONE)
 		dlog_print(DLOG_DEBUG, TAG, "http_transaction_submit failed: %d", ret);


 	/*
 	ret = HTTP_ERROR_NONE;
 	http_status_code_e status_code = HTTP_STATUS_UNDEFINED;

 	ret = http_transaction_response_get_status_code(transaction, &status_code);
 	if (ret != HTTP_ERROR_NONE)
 		dlog_print(DLOG_DEBUG, TAG, "http_transaction_response_get_status_code failed: %d", ret);


 	dlog_print(DLOG_DEBUG, TAG, "HTTP STATUS: %d", status_code);

 	ret = HTTP_ERROR_NONE;

 	ret = http_transaction_destroy(transaction);
 	if (ret != HTTP_ERROR_NONE)
 		dlog_print(DLOG_DEBUG, TAG, "http_transaction_destroy failed: %d", ret);
 	*/

 	SEND_REQUEST = false;

 	return true;
  }


  static void example_sensor_callback(sensor_h sensor, sensor_event_s *event, void *user_data)
  {
  	/*
  	If a callback is used to listen for different sensor types,
  	it can check the sensor type
  	*/
  	sensor_type_e type;
  	sensor_get_type(sensor, &type);

  	Stage stage = Stage::GetCurrent();
  	std::string text;
	//dlog_print(DLOG_DEBUG, TAG, "SENSOR CALLBACK HIT");

  	switch(type)
  	{
  		case SENSOR_LIGHT:
  		{
  			float lux = event->values[0];
  			int luxInt = (int)floor(lux);
  			text = std::to_string(luxInt);
  		    break;
  		}
  		case SENSOR_ACCELEROMETER:
  		{
  			text = "CUNT FUCK";
  		    dlog_print(DLOG_DEBUG, TAG, "GO FAST");
  			break;
  		}
  		case SENSOR_HRM:
  		{
  			int beats = event->values[0];
  			text = std::to_string(beats);
  		    dlog_print(DLOG_DEBUG, TAG, "BEATS %d", beats);
  			break;
  		}
  		case SENSOR_HRM_LED_GREEN:
  		{
  			int beats = event->values[0];
			text = std::to_string(beats);
			dlog_print(DLOG_DEBUG, TAG, "GREEN BEATS %d", beats);
  			break;
  		}
  		case SENSOR_PRESSURE:
		{
			float pressure = event->values[0];
			text = std::to_string((int)floor(pressure));
			dlog_print(DLOG_DEBUG, TAG, "PRESSURE %f", pressure);
			break;
		}
  		default:
  		{
  			break;
  		}
  	}

	if(SEND_REQUEST)
	{
		SendInfo(text);
	}

  	if(INSTNACE != NULL)
  	{
  		TextLabel& textLabel = INSTNACE->mText;

		stage.Remove( textLabel );
		textLabel = TextLabel::New( text.c_str() );
		textLabel.SetSize( stage.GetSize() );
		textLabel.SetAnchorPoint( AnchorPoint::TOP_LEFT );
		textLabel.SetProperty( TextLabel::Property::HORIZONTAL_ALIGNMENT, "CENTER" );
		textLabel.SetProperty( TextLabel::Property::VERTICAL_ALIGNMENT, "CENTER" );
		stage.Add( textLabel );
  	}
  }


  // The init signal is received once (only) during the Application lifetime
void Create( Application& application )
{

    int integer = 21;
    char string[] = "test dlog";

	dlog_print(DLOG_DEBUG, TAG, "CREATE ENTERED");

	INSTNACE = this;
	SEND_REQUEST = true;
	mText = TextLabel::New( "DATA HERE" );

	Stage stage = Stage::GetCurrent();
	stage.SetBackgroundColor( Color::WHITE );

	bool supported = false;

	auto sensorType = SENSOR_TYPE;

	std::string text;

	sensor_is_supported(sensorType, &supported);
	if (!supported) {
		text = "NO SENSOR";
	}
	else
	{
		text = "SENSOR";
	}

	TextLabel textLabel = TextLabel::New( text.c_str() );
	textLabel.SetSize( stage.GetSize() );
	textLabel.SetAnchorPoint( AnchorPoint::TOP_LEFT );
	textLabel.SetProperty( TextLabel::Property::HORIZONTAL_ALIGNMENT, "CENTER" );
	textLabel.SetProperty( TextLabel::Property::VERTICAL_ALIGNMENT, "TOP" );
	stage.Add( textLabel );



	// Connect to touch & key event signals
	stage.GetRootLayer().TouchedSignal().Connect( this, &HelloWorldController::OnTouch );
	stage.KeyEventSignal().Connect( this, &HelloWorldController::OnKeyEvent );

	dlog_print(DLOG_DEBUG, TAG, "CREATE LEFT");
}

  bool OnTouch( Actor actor, const TouchEvent& event )
  {
    // Quit the application
    //mApplication.Quit();
    return true;
  }

  void OnKeyEvent( const KeyEvent& event )
  {
    if( event.state == KeyEvent::Down )
    {
      if( IsKey( event, DALI_KEY_ESCAPE ) || IsKey( event, DALI_KEY_BACK ) )
      {
        //mApplication.Quit();
      }
    }
  }

private:
  Application&  mApplication;
};

HelloWorldController* HelloWorldController::INSTNACE = (HelloWorldController*)0;
bool HelloWorldController::SEND_REQUEST = true;
sensor_type_e HelloWorldController::SENSOR_TYPE = SENSOR_LIGHT;

// Entry point for Tizen applications
//
int main( int argc, char **argv )
{
	Application application = Application::New( &argc, &argv );
	HelloWorldController test( application );

	dlog_print(DLOG_DEBUG, TAG, "HTTP INIT STARTING");
	int ret = HTTP_ERROR_NONE;

	ret = http_init();
	if (ret != HTTP_ERROR_NONE)
		dlog_print(DLOG_DEBUG, TAG, "INIT FAILLED");

	dlog_print(DLOG_DEBUG, TAG, "HTTP INIT DONE");

	sensor_h sensor;
	sensor_get_default_sensor(HelloWorldController::SENSOR_TYPE, &sensor);

	sensor_listener_h listener;
	sensor_create_listener(sensor, &listener);

	sensor_listener_set_event_cb(listener, 1000, HelloWorldController::example_sensor_callback, NULL);
	sensor_listener_set_attribute_int(listener, SENSOR_ATTRIBUTE_PAUSE_POLICY, SENSOR_PAUSE_NONE);

	sensor_listener_start(listener);


	application.MainLoop();
	return 0;
}
