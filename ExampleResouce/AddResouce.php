<?php
    session_start();

    require_once('policyFunc.php');

    if(isset($_GET['resid'])) {
        $resID = $_GET['resid'];
        
        $apiKey = '9A38075E807090757AAA40FF9470B499D63A56E01BB92F680E3EE09A25DE9D994AFB4E2B940A8197107A5D33C9CC364A5147D79F39C76439E90B4A3A224890C97807849810386707836B23B8C99FF5389AA94659792D';
        
        $policy = '
            {
                "author": "Paul",
                "proity": 0,
                "decision": "test",
                "dataConsumer": {
                    "id": 0,
                    "name": "NotPaul",
                    "value": "pfsarda23@gmail.com"
                },
                "jsonCompositeContex": {
                    "id": 0,
                    "conteiexs": [
                        {
                            "id": 0,
                            "contex": {
                                "id": 0,
                                "name": "TempTest",
                                "interval": 0,
                                "contextProvider": {
                                    "$type": "PrototypeContexProvider.src.TempurtreContexProvider, PrototypeContexProvider",
                                    "cityID": "6952201",
                                    "selectedMessurement": 1
                                },
                                "givenValue": 15,
                                "IContexOperator": {
                                    "$type": "PrototypeContexProvider.src.ContexGreaterThan, PrototypeContexProvider"
                                }
                            },
                            "glue": 0,
                            "not": false
                        }
                    ]
                },
                "privacyOblgations": {
                    "id": 0,
                    "purpose": "Testing",
                    "granularity": "Garbage",
                    "anonymisation": "Garbage",
                    "notifaction": "Garbage",
                    "accounting": "Garbage"
                },
                "resharingObligations": {
                    "id": 0,
                    "canShare": false,
                    "cardinality": 10,
                    "recurring": 10
                }
            }
            ';
            
            echo $policy . '</br>Fuck</br>';
            
            echo AddPolicy('localhost:44320', $apiKey, $resID, $policy);
        }
        else
        {
            echo 'Missing resID';
        }
?>
