using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace KodeksoKlausimynas
{
    public partial class Main : Form
    {
        public const string VERSION =  "Version 1.0.1";

        public const int SPEED_FINE_LOW = 150;
        public const int SPEED_FINE_MEDIUM = 400;
        public const int SPEED_FINE_HIGH = 1000;
        public const int INTERSECTION_FINE = 100;
        public const int TRAFFIC_LIGHT_FINE = 100;
        public const int NOT_SAFE_DRIVING_SIMPLE_FINE = 150;
        public const int NOT_SAFE_DRIVING_AUTOBAN_FINE = 750;
        public const int DELIBERATE_DRIVING_SIMPLE_FINE = 500;
        public const int DELIBERATE_DRIVING_AUTOBAN_FINE = 1000;
        public const int WRONG_SIDE_SIMPLE_FINE = 150;
        public const int WRONG_SIDE_AUTOBAN_FINE = 1000;
        public const int ILLEGAL_CARGO_FINE = 150;
        public const int DRUGS_FINE = 1000;
        public const int NUMBER_PLATE_FINE = 450;
        public const int INSURANCE_FINE = 200;
        public const int CON0 = 0;
        public const int CON1 = 300;
        public const int CON2 = 500;
        public const int CON3 = 1500;
        public const int CON4 = 2000;
        public const int BANK_FINE = 720;
        public const int WORKPLACE_FINE = 430;

        String answer;

        public Main()
        {
            //AllocConsole();
            InitializeComponent();
            createQuestion();
            fineBox.Text = "";
            versionText.Text = VERSION;
        }

        private void generateQuestion_Click(object sender, EventArgs e)
        {
             generateNew();
        }

        private void generateNew()
        {
            {
                createQuestion();
                if (instantShow.Checked)
                {
                    fineBox.Text = answer;
                }
                else
                {
                    fineBox.Text = "";
                }

            }
        }

        private void showAnswer_Click(object sender, EventArgs e)
        {
            fineBox.Text = answer;
        }

        private void createQuestion()
        {

            int[] startingChase = new int[] { -1, -1 }; // Two values if there are more then 1 reason to chase
            int[] speed = new int[] { -1, -1 };
            int bustedCON = -1;
            int joinCON = -1;
            int money;
            int licenzes;
            int robbed = 0;
            bool newChase = false;
            bool autoBan = false;
            bool carPlates = false;
            bool insurance = false;
            bool illegal = false;
            bool drugs = false;
            bool rob = false;
            bool bank = false;
            int fine = 0;



            // This Random will be sent to every function, as the program runs fast with so many randoms, creating new random
            // would end with the same result.
            Random generator = new Random();

            bustedCON = generateCON(generator, 0, 5);
            carPlates = generateCarPlatesChance(generator);
            insurance = generateInsuranceChance(generator);
            illegal = generateIllegal(generator);
            drugs = generateDrugs(generator);
            money = generateMoney(generator);
            licenzes = generatelicenzes(generator);
            rob = generateRob(generator);
            if (rob) { bank = generateIsBank(generator); }


            if (rob)
            {
                robbed = generateRobAmmount(generator, bank);
            }
            else
            {


                newChase = generator.Next(0, 3) > 0; // 0 - new, 1 - old
                if (newChase)
                {
                    generateNewChase(ref autoBan, ref startingChase, ref speed, generator);
                }
                else
                {
                    if (bustedCON > 1)
                    {
                        do
                        {
                            joinCON = generateCON(generator, 1, 5);
                        }
                        while (bustedCON < joinCON);
                    }
                    else
                    {
                        generateNewChase(ref autoBan, ref startingChase, ref speed, generator);
                        newChase = true;
                    }
                }
            }
            //Console.WriteLine();
            //Console.WriteLine("New");
            //Console.WriteLine("rob: " + rob);
            //Console.WriteLine("bank: " + bank);
            //Console.WriteLine("BustedCON: " + bustedCON);
            //Console.WriteLine("carPlates: " + carPlates);
            //Console.WriteLine("insurance: " + insurance);
            //Console.WriteLine("illegal: " + illegal);
            //Console.WriteLine("drugs: " + drugs);
            //Console.WriteLine("money: " + money);
            //Console.WriteLine("newChase: " + newChase);
            //Console.WriteLine("joinCON: " + joinCON);
            //Console.WriteLine("startingChase: " + startingChase[0] + " " + startingChase[1]);
            //Console.WriteLine("speed: " + speed[0] + " " + speed[1]);

            // ***********************************************************************************
            // Creating the question
            // ***********************************************************************************
            StringBuilder buildQuestion = new StringBuilder();

            if (rob)
            {
                buildQuestion.Append("Vagis iš");
                if (bank)
                {
                    buildQuestion.Append(" banko");
                }
                else
                {
                    buildQuestion.Append(" darbovietės");
                }
                buildQuestion.Append(" pavogė ");
                buildQuestion.Append(robbed);
            }
            else
            {
                if (newChase)
                {
                    buildQuestion.Append("Pradėjai gaudyti");
                    if (autoBan)
                    {
                        buildQuestion.Append(", autobane,");
                    }
                    buildQuestion.Append(" už ");
                    buildQuestion.Append(newChaseNaming(startingChase[0]));
                    if (startingChase[0] == 0)
                    {
                        buildQuestion.Append(" (" + speed[1] + "/" + speed[0] + ")Km/h");
                    }
                    if (startingChase[1] != -1)
                    {
                        buildQuestion.Append(" ir ");
                        buildQuestion.Append(newChaseNaming(startingChase[1]));
                        if (startingChase[1] == 0)
                        {
                            buildQuestion.Append(" (" + speed[1] + "/" + speed[0] + ")Km/h");
                        }
                    }
                }
                else
                {
                    buildQuestion.Append("Prisijungei prie gaudynių per ");
                    buildQuestion.Append(generateCONName(joinCON));
                }
            }
            buildQuestion.Append(", pagavai per ");


            if (rob)
            {
                bustedCON = generateCON(generator, 2, 5);
            }
            buildQuestion.Append(generateCONName(bustedCON));
            buildQuestion.Append(". Jis turi ");
            buildQuestion.Append(licenzes);
            buildQuestion.Append(" licenzijas ir ");
            buildQuestion.Append(money);
            buildQuestion.Append(" eurus.");

            if (carPlates)
            {
                if (numberGenerator(generator, 0, 2) == 0)
                {
                    buildQuestion.Append(" Neturi numerių");
                }
                else
                {
                    buildQuestion.Append(" Blogi numeriai");
                }
            }

            if (insurance)  // Generating a chance for not having insurance. 33% of not having
            {
                if (carPlates)
                {
                    if (numberGenerator(generator, 0, 2) == 0)
                    {
                        buildQuestion.Append(" ir nėra draudimo");
                    }
                    else
                    {
                        buildQuestion.Append(" ir negaliojantis draudimas");
                    }
                }
                else
                {
                    if (numberGenerator(generator, 0, 2) == 0)
                    {
                        buildQuestion.Append(" Nėra draudimo");
                    }
                    else
                    {
                        buildQuestion.Append(" Negaliojantis draudimas");
                    }
                }
            }

            if (illegal)
            {
                buildQuestion.Append(". Nelegalus krovinys");
                if (drugs) { buildQuestion.Append(" ir narkotikai"); }
            }
            if (drugs && !illegal) { buildQuestion.Append(". Narkotikai"); }

            buildQuestion.Append(". Kokia bauda?");

            String question = buildQuestion.ToString();
            QuestionText.Text = question;






            // ***********************************************************************************
            // Calculating fine
            // ***********************************************************************************

            StringBuilder explanation = new StringBuilder();

            fine += getCONFine(bustedCON);
            explanation.Append("CON" + bustedCON + " = " + getCONFine(bustedCON));

            if (newChase)
            {
                if (startingChase[0] != -1)
                {
                    calculateNewChaseFines(autoBan, bustedCON, startingChase[0], licenzes, speed, ref fine, ref explanation);
                }
                if (startingChase[1] != -1)
                {
                    calculateNewChaseFines(autoBan, bustedCON, startingChase[1], licenzes, speed, ref fine, ref explanation);
                }
            }

            if (insurance)
            {
                if (rob)
                {
                    explanation.Append("\r\nDraudimas = 0. Nepridedami vogimo atveju");
                }
                else
                {
                    bool checkFine = true;
                    if (bustedCON == 4)
                    {
                        explanation.Append("\r\nDraudimas = 0. Prisideda prie CON0 - CON3");
                        checkFine = false;
                    }
                    if (licenzes < 20)
                    {
                        explanation.Append("\r\nDraudimas = 0. Neskiriama turint mažiau nei 20 licenzijų");
                        checkFine = false;
                    }
                    if (money < 100)
                    {
                        explanation.Append("\r\nDraudimas = 0. Neskiriama neturint ~100 EUR (Apytiksli draudimo vienai dienai kaina)");
                        checkFine = false;
                    }
                    if (checkFine)
                    {
                        fine += returnInsuranceFine(licenzes);
                        explanation.Append("\r\nDraudimas = " + returnInsuranceFine(licenzes) + ". Prisideda prie CON0 - CON3");
                    }
                }
                
            }

            if (carPlates)
            {
                if (rob)
                {
                    explanation.Append("\r\nNumeriai = 0. Nepridedami vogimo atveju");
                }
                else
                {
                    bool checkFine = true;
                    if (bustedCON == 4)
                    {
                        explanation.Append("\r\nNumeriai = 0. Prisideda prie CON0 - CON3");
                        checkFine = false;
                    }
                    if (licenzes < 20)
                    {
                        explanation.Append("\r\nNumeriai = 0. Neskiriama turint mažiau nei 20 licenzijų");
                        checkFine = false;
                    }
                    if (money < 150)
                    {
                        explanation.Append("\r\nNumeriai = 0. Neskiriama neturint 150 EUR (Paprastų numerių kaina)");
                        checkFine = false;
                    }
                    if (checkFine)
                    {
                        fine += returnCarPlatesFine();
                        explanation.Append("\r\nNumeriai = " + returnCarPlatesFine() + ". Prisideda prie CON0 - CON3");
                    }
                }
                
            }
            
            if (illegal)
            {
                if (rob)
                {
                    explanation.Append("\r\nNelegalus krovinys = 0. Nepridedamas vogimo atveju");
                }
                else
                {
                    fine += ILLEGAL_CARGO_FINE;
                    explanation.Append("\r\nNelegalus krovinys = " + ILLEGAL_CARGO_FINE + ". Prisideda prie visų CON");
                }

            }
            if (drugs)
            {
                if (rob)
                {
                    explanation.Append("\r\nNarkotikai = 0. Nepridedami vogimo atveju");
                }
                else
                {
                    fine += DRUGS_FINE;
                    explanation.Append("\r\nNarkotikai = " + DRUGS_FINE + ". Prisideda prie visų CON");
                }

            }

            if (rob)
            {
                if (bank)
                {
                    explanation.Append("\r\nBauda už pavogtą banką = " + (robbed + BANK_FINE));
                    explanation.Append(". " + robbed + " + " + BANK_FINE);
                    robbed += BANK_FINE;
                    
                }
                else
                {
                    explanation.Append("\r\nBauda už pavogtą darbovietę = " + (robbed + WORKPLACE_FINE));
                    explanation.Append(". " + robbed + " + " + WORKPLACE_FINE);
                    robbed += WORKPLACE_FINE;
                }

                if (robbed > getCONFine(bustedCON))
                {
                    fine -= getCONFine(bustedCON);
                    fine += robbed;
                    explanation.Append("\r\nBauda už vogimą = " + robbed + ". Viršija minimalią CON baudą");
                }
                else
                {
                    explanation.Append("\r\nBauda už vogimą = " + robbed + ". Neviršija minimalios CON baudos");
                }
            }
            explanation.Append("\r\nBauda = " + fine);
            answer = explanation.ToString();

        }

        private void generateNewChase(ref bool autoBan, ref int[] startingChase, ref int[] speed, Random generator)
        {
            autoBan = generateAutoban(generator);

            startingChase[0] = this.newChase(generator, autoBan);

            if (numberGenerator(generator, 0, 2) == 1) // if 1, there are two reasons for chase
            {
                do
                {
                    startingChase[1] = this.newChase(generator, autoBan);
                }
                while (startingChase[0] == startingChase[1]);
            }

            if (startingChase[0] == 0 || startingChase[1] == 0)
            {
                speed = generateSpeeds(generator, speed);
            }
        }

        // 0. Greitis
        // 1. Sąnkryža
        // 2. Šviesaforas
        // 3. Neatsargus
        // 4. Chiliganiškas
        // 5. Priešinga juosta
        private int newChase(Random generator, bool autoban)
        {
            
            int number;
            if (autoban)
            {
                number = numberGenerator(generator, 3, 6);
            }
            else
            {
                number = numberGenerator(generator, 0, 6);
            }

            return number;

        }

        String newChaseNaming(int reasonNumber)
        {
            //Console.WriteLine("Naming: " + reasonNumber);
            if (reasonNumber >= 0 && reasonNumber < 6)
            {
                String[] reasons = { "viršytą greitį",
                                "sąnkryžą",
                                "šviesaforą",
                                "neatsargų vairavimą",
                                "chuliganišką vairavimą",
                                "priešingą eismo juostą"};
                return reasons[reasonNumber];
            }
            else
            {
                return "Wrong chase reason naming";
            }
        }
        // 0. Greitis
        // 1. Sąnkryža
        // 2. Šviesaforas
        // 3. Neatsargus
        // 4. Chiliganiškas
        // 5. Priešinga juosta
        //int returnChaseReasonFine(int reason)
        //{
        //    int[] fines = {}
        //}

        int joinChase(Random generator)
        {
            return generateCON(generator, 1, 5);
        }

        private int[] generateSpeeds(Random generator, int[] speeds)
        {
            speeds[0] = generator.Next(50, 150);
            speeds[1] = generator.Next(speeds[0], speeds[0] + 70);
            return speeds;
        }


        private int generateCON(Random generator, int from, int to)
        {
            return numberGenerator(generator, from, to);
        }

        // 0. CON0
        // 1. CON1
        // 2. CON2
        // 3. CON3
        // 4. CON4
        private String generateCONName(int state)
        {
            //Console.WriteLine("CON Naming: " + state);
            if (state >= 0 && state < 5)
            {
                String[] allCONS = { "CON0", "CON1", "CON2", "CON3", "CON4" };
                return allCONS[state];
            }
            else
            {
                return "Something went wrong with CON naming";
            }
        }

        private bool generateAutoban(Random generator)
        {
            return generator.Next(0, 3) == 0;
        }

        private int getCONFine(int state)
        {
            if (state >= 0 && state < 5)
            {
                int[] fines = { CON0, CON1, CON2, CON3, CON4 };
                return fines[state];
            }
            else
            {
                return -1;
            }
        }

        private int generatelicenzes(Random generator)
        {
            int lowlicenze = numberGenerator(generator, 0, 4); // Getting a chanse of having lower than 30 licenzes

            if (lowlicenze == 0) // 25% change of getting low licenzes driver
            {
                return numberGenerator(generator, 1, 30);
            }
            else
            {
                return numberGenerator(generator, 30, 2000);
            }
        }

        /// <summary>
        /// This function returns the fine for different amount of licenzes.
        /// </summary>
        /// <param name="licenzes"></param>
        /// <returns int="fine"</returns>
        private int returnInsuranceFine(int licenzes)
        {
            if (licenzes < 1001)
            {
                if (licenzes < 501)
                {
                    return INSURANCE_FINE * 1;
                }
                else
                {
                    return INSURANCE_FINE * 2;
                }
            }
            else
            {
                if (licenzes < 1501)
                {
                    return INSURANCE_FINE * 3;
                }
                else
                {
                    return INSURANCE_FINE * 4;
                }
            }
        }

        private int generateMoney(Random generator)
        {
            int lowlicenze = numberGenerator(generator, 0, 3); // Getting a chanse of having low money

            if (lowlicenze == 0) // 25% change of getting low money driver
            {
                return numberGenerator(generator, 20, 100);
            }
            else
            {
                return numberGenerator(generator, 200, 20000);
            }
        }
        private bool generateInsuranceChance(Random generator)
        {
            return generator.Next(0, 3) == 0;
        }

        private bool generateCarPlatesChance(Random generator)
        {
            return generator.Next(0, 3) == 0;
        }


        private int returnCarPlatesFine()
        {
            return NUMBER_PLATE_FINE;
        }

        int numberGenerator(Random generator, int from, int to)
        {
            return generator.Next(from, to);
        }

        private bool generateIllegal(Random generator)
        {
            return generator.Next(0, 4) == 0;
        }
        private bool generateDrugs(Random generator)
        {
            return generator.Next(0, 4) == 0;
        }


        private void calculateNewChaseFines(bool autoBan, int bustedCON, int startingChase, int licenzes, int[] speed, ref int fine, ref StringBuilder explanation)
        {
            if (autoBan)
            {
                if (startingChase == 3)
                {
                    if (licenzes >= 20)
                    {
                        fine += NOT_SAFE_DRIVING_AUTOBAN_FINE;
                        explanation.Append("\r\nNeatsargus autobane = " + NOT_SAFE_DRIVING_AUTOBAN_FINE + ". Prisideda prie visų CON");
                    }
                    else
                    {
                        fine += NOT_SAFE_DRIVING_SIMPLE_FINE;
                        explanation.Append("\r\nNeatsargus autobane = " + NOT_SAFE_DRIVING_SIMPLE_FINE + ". Mažesnė bauda, nes neturi 20 licenzijų");
                    }

                }
                if (startingChase == 4)
                {
                    if (licenzes >= 20)
                    {
                        fine += DELIBERATE_DRIVING_AUTOBAN_FINE;
                        explanation.Append("\r\nChuliganiškas autobane = " + DELIBERATE_DRIVING_AUTOBAN_FINE + ". Prisideda prie visų CON");
                    }
                    else
                    {
                        fine += DELIBERATE_DRIVING_SIMPLE_FINE;
                        explanation.Append("\r\nChuliganiškas autobane = " + DELIBERATE_DRIVING_SIMPLE_FINE + ". Mažesnė bauda, nes neturi 20 licenzijų");
                    }
                }
                if (startingChase == 5)
                {
                    if (licenzes >= 20)
                    {
                        fine += WRONG_SIDE_AUTOBAN_FINE;
                        explanation.Append("\r\nPriešinga eismo juosta autobane = " + WRONG_SIDE_AUTOBAN_FINE + ". Prisideda prie visų CON");
                    }
                    else
                    {
                        fine += WRONG_SIDE_SIMPLE_FINE;
                        explanation.Append("\r\nPriešinga eismo juosta autobane = " + WRONG_SIDE_SIMPLE_FINE + ". Mažesnė bauda, nes neturi 20 licenzijų");
                    }
                }
            }
            else
            {
                if (startingChase == 0)
                {
                    int difference = speed[1] - speed[0];
                    if (difference < 10)
                    {
                        if (bustedCON < 3)
                        {
                            explanation.Append("\r\nGreitis = 0. Duodamas įspėjimas iki 10km/h");
                        }
                        else
                        {
                            explanation.Append("\r\nGreitis = 0. Prisideda prie CON0 - CON2");
                        }

                    }
                    if (difference >= 10 && difference < 31)
                    {
                        if (bustedCON < 3)
                        {
                            fine += SPEED_FINE_LOW;
                            explanation.Append("\r\nGreitis = " + SPEED_FINE_LOW + ". Skiriama viršijant 10-30km/h. Prisideda prie CON0 - CON2");
                        }
                        else
                        {
                            explanation.Append("\r\nGreitis = 0. Prisideda prie CON0 - CON2");
                        }
                    }
                    if (difference >= 31 && difference < 51)
                    {
                        if (bustedCON < 3)
                        {
                            fine += SPEED_FINE_MEDIUM;
                            explanation.Append("\r\nGreitis = " + SPEED_FINE_MEDIUM + ". Skiriama viršijant 31-50km/h. Prisideda prie CON0 - CON2");
                        }
                        else
                        {
                            explanation.Append("\r\nGreitis = 0. Prisideda prie CON0 - CON2");
                        }
                    }
                    if (difference >= 51)
                    {
                        if (bustedCON < 3)
                        {
                            fine += SPEED_FINE_HIGH;
                            explanation.Append("\r\nGreitis = " + SPEED_FINE_HIGH + ". Skiriama viršijant >51km/h. Prisideda prie CON0 - CON2");
                        }
                        else
                        {
                            explanation.Append("\r\nGreitis = 0. Prisideda prie CON0 - CON2");
                        }
                    }

                }
                if (startingChase == 1)
                {
                    if (bustedCON > 0)
                    {
                        explanation.Append("\r\nSąnkryža = 0. Prisideda tik prie CON0");
                    }
                    else
                    {
                        fine += INTERSECTION_FINE;
                        explanation.Append("\r\nSąnkryža = " + INTERSECTION_FINE + ". Prisideda tik prie CON0");
                    }

                }
                if (startingChase == 2)
                {
                    if (bustedCON > 0)
                    {
                        explanation.Append("\r\nŠviesaforas = 0. Prisideda tik prie CON0");
                    }
                    else
                    {
                        fine += TRAFFIC_LIGHT_FINE;
                        explanation.Append("\r\nŠviesaforas = " + TRAFFIC_LIGHT_FINE + ". Prisideda tik prie CON0");
                    }

                }
                if (startingChase == 3)
                {
                    if (bustedCON > 0)
                    {
                        explanation.Append("\r\nNeatsargus vairavimas = 0. Prisideda tik prie CON0");
                    }
                    else
                    {
                        fine += NOT_SAFE_DRIVING_SIMPLE_FINE;
                        explanation.Append("\r\nNeatsargus vairavimas = " + NOT_SAFE_DRIVING_SIMPLE_FINE + ". Prisideda tik prie CON0");
                    }

                }
                if (startingChase == 4)
                {
                    if (bustedCON > 2)
                    {
                        explanation.Append("\r\nChuliganiškas = 0. Prisideda prie CON0 - CON2");
                    }
                    else
                    {
                        fine += DELIBERATE_DRIVING_SIMPLE_FINE;
                        explanation.Append("\r\nChuliganiškas = " + DELIBERATE_DRIVING_SIMPLE_FINE + ". Prisideda prie CON0 - CON2");
                    }

                }
                if (startingChase == 5)
                {
                    if (bustedCON > 0)
                    {
                        explanation.Append("\r\nPriešinga eismo juosta = 0. Prisideda tik prie CON0");
                    }
                    else
                    {
                        fine += WRONG_SIDE_SIMPLE_FINE;
                        explanation.Append("\r\nPriešinga eismo juosta = " + WRONG_SIDE_SIMPLE_FINE + ". Prisideda tik prie CON0");
                    }

                }
            }
        }

        private bool generateRob(Random generator)
        {
            return generator.Next(0, 5) == 0;
        }
        private bool generateIsBank(Random generator)
        {
            return generator.Next(0, 3) > 0; // 0 Means it was a bank
        }
        private int generateRobAmmount(Random generator, bool isBank)
        {
            if (isBank)
            {
                return generator.Next(800, 4000);
            }
            else
            {
                return generator.Next(300, 1000);
            }
        }



        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool AllocConsole();
    }
}
