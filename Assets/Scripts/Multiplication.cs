// libraries used
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class Multiplication : MonoBehaviour
{
    // Serialized Fields for Unity Editor
    [SerializeField]
    private Button Retry_button ; // Button which restarts game
    [SerializeField]
    private Image Retry; // Retry Screen Background Image
    [SerializeField]
    private Image RFrame; // Retry Screen Frame Image
    [SerializeField]
    private TMP_Text Time1; // Text that displays time
    [SerializeField]
    private TMP_Text Feedback; // Text displaying feedback to user input
    [SerializeField]
    private TMP_Text num1; // Text displaying number 1
    [SerializeField]
    private TMP_Text num2; // Text displaying number 2
    [SerializeField]
    private TMP_InputField answer; // Input Field that collects answer from user
    [SerializeField]
    private TMP_Text Total; // Text that displays total number of questions answered
    [SerializeField]
    private TMP_Text Incorrect; //  Text displaying number of correct answers
    [SerializeField]
    private TMP_Text Correct; // Text displaying number of incorrect answers
    
    // images and sprites o for feedback image
    public Image Reaction; //Image that changes according to feedback
    public Sprite Right; 
    public Sprite Wrong;
    public Sprite Question;

    //variable for multiplication logic
    int numb1; // contains random number
    int numb2; // contains random number
    
    //variables for number display
    string t1; // text displaying numb1
    string t2; // text displaying numb2
    
    //Time variables
    public float timeremain = 30; // variable that specifies amount of time given to user
    public float minutes; // used to store minutes value 
    public float seconds; //  used to store seconds value
    public bool timer = false; // used to stop timer (true = time continues / false = time pauses)
    public bool game = true; // used to stop game (true = game continues / false = game pauses)

    //Stat variables
    public int num_att = 0; // variable storing number of attended questions
    public int num_cor = 0; //  variable storing nnumber of correct quesstions
    public int num_incor = 0; // variable storing number of incorrect questions

    //Input Checking Variable
    bool ErrorCheck; // used to check if given input is purely numerical (true = purely numerical / false = alphanumerical)

    // Start is called before the first frame update
    void Start()
    {
        //Setting Iniatial State by hiding retry elements
        Retry.gameObject.SetActive(false); // hides retry screen
        Retry_button.gameObject.SetActive(false); // hides retry button
        RFrame.gameObject.SetActive(false); // hides retry frame

        //Generating intial multiplication factors
        numb1 = Random.Range(0,13); // assigns random number to numb1
        numb2 = Random.Range(0,13); // assigns random number to numb2
        t1 = numb1.ToString(); // numb1 value is converted to string and saved to t1
        t2 = numb2.ToString(); // numb2 value is converted to string and saved to t2
        num1.text = t1; // displaying string value
        num2.text = t2; // displaying string value

        //Starting Game and Timer
        timer = true; // starting timer
        game = true; // starting game
    }

    // Update is called once per frame
    void Update()
    {
        if(game){ //(true = game starts/ false = game stops)

            if(timer){ //(true = timer starts/ false = timer stops)

                if (timeremain > 0){ // only goes in if there is time remaining 
                    if (game){ //(true = game starts/ false = game stops)
                    }
                    // Update timer display
                    timeremain -= Time.deltaTime; // changes timeremaining by subtracting time spent from remaining time
                    minutes = Mathf.FloorToInt(timeremain/60); // converts seconds to minutes and stores in minutes variable
                    seconds = Mathf.FloorToInt(timeremain%60); // stores remaining seconds to seconds 
                    Time1.text = "Time: "+minutes+":"+seconds; // displays time in minutes and seconds
                }
                else{
                    // Display stats when timer is over
                    minutes = 0; // setting to 0 to prevent time going to negative
                    seconds = 0; // setting to 0 to prevent time going to negative
                    Time1.text = "Time: "+minutes+":"+seconds; // displaying time
                    timer = false; // stop timer but not game
                    Feedback.text = "Time Over: \nStats \nAttended: "+num_att+"\nCorrect: "+num_cor+"\nIncorrect: "+num_incor; // display feedback
                    Retry.gameObject.SetActive(true); // show retry screen
                    Retry_button.gameObject.SetActive(true); // show retry button
                    RFrame.gameObject.SetActive(true); // show retry frame
                }
            }
        }    
    }

    // Function to check answer(connected to button in unity)
    public async void Check(){
        // Checks whether given input valid (is purely nunmerical)
        int value; // temp variable to store user string input
        string i = answer.text; // storing user input
        ErrorCheck = int.TryParse(i, out value); // checking if input is purely numerical
        if(ErrorCheck){ // (true = purely numerical / false = alphanumerical)
            if(timer){ // (true = time continues / false = time pauses)
                // increments number of attended questions irespective of result
                num_att += 1; // increases number of attended by 1
                Total.text = "Questions Answered: "+num_att;// displays number attended
                // checking answer
                int a = numb1*numb2; // calculate answer to check user input
                // Display result for correct input
                if (value == a){ // if correct
                    Debug.Log("Correct"); // console debug 
                    Feedback.text = "\nYou are Correct";// display feedback
                    Reaction.sprite = Right;// change inmage to correct
                    num_cor += 1; // increase number of correct by 1
                    Correct.text = "Correct: "+num_cor; // display number of correct
                }
                else { // if incorrect
                // Display result for incorrect input
                    Debug.Log("Incorrect"); // console debug
                    Feedback.text = "\nYou are Incorrect"; // display feedback
                    Reaction.sprite = Wrong; //  change image to incorrect
                    num_incor += 1; // increase number of incorrect by 1
                    Incorrect.text = "Incorrect: "+num_incor; // display number of incorrect
                }
                //Pause game briefly
                game = false; //(true = game starts/ false = game stops)
                await Task.Delay(1500); // delay for 1.5 seconds
                game = true; //(true = game starts/ false = game stops)

                // Reset for next question
                answer.text = ""; // clearing input field
                Feedback.text = "\nEnter the product of the two given numbers"; //  reseting feedback
                Reaction.sprite = Question; //  reseting feedback image 
                numb1 = Random.Range(0,13); // assigning new random number
                numb2 = Random.Range(0,13); // assigning new random number
                t1 = numb1.ToString(); // numb1 value is converted to string and saved to t1
                t2 = numb2.ToString(); // numb2 value is converted to string and saved to t2
                num1.text = t1; // displaying string value
                num2.text = t2; // displaying string value
            }

        }
        else{
            // Display result for invalid input
            Feedback.text = "\nNot Valid input"; // display invalid input feedback
        }
        
    }
}
