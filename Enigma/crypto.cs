using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Enigma
{
    public enum rotaNames{ //rota names
            rotaOne,
            rotaTwo,
            rotaThree,
            rotaFour,
            rotaFive
    }

    class crypto
    {
        private rota RotaOne;
        private rota RotaTwo;
        private rota RotaThree;
        private rota RotaFour;
        private rota RotaFive;
        private List<rota> RotaSelection = new List<rota>();
        private short rotaOneRevolutionCount = 1; //counts the revoultions of each rota
        private short rotaTwoRevolutionCount = 1;
        private short rotaThreeRevolutionCount = 1;

        public crypto()//generate Rotas
        {
            Char[] PinPositions = {'C','T','E','G','V','W','J','X','M','K','L','D','I','Z','S','R','P','B','N','A','F','Y','H','U','O','Q'};
            RotaOne = new rota(rotaNames.rotaOne, PinPositions);

            Char[] PinPositions2 = {'Z','P','T','D','O','F','Y','J','E','G','A','S','I','V','B','R','L','K','H','U','Q','M','W','N','C','X'};
            RotaTwo = new rota(rotaNames.rotaTwo, PinPositions2);

            Char[] PinPositions3 = {'K','P','S','W','Y','D','Z','E','B','Q','N','R','H','F','G','U','V','X','L','A','I','O','T','C','M','J'};
            RotaThree = new rota(rotaNames.rotaThree, PinPositions3);

            Char[] PinPositions4 = {'V','T','D','K','N','A','O','Y','B','Q','I','U','C','G','H','R','L','M','E','P','X','S','J','W','F','Z'};
            RotaFour = new rota(rotaNames.rotaFour, PinPositions4);

            Char[] PinPositions5 = {'X','W','L','H','C','M','U','Y','P','J','N','Q','R','A','T','E','F','O','S','K','Z','D','B','I','V','G'};
            RotaFive = new rota(rotaNames.rotaFive, PinPositions5);
        }

        public void ThreeRotaSelections(rotaNames[] rotaSelectionArray,short[] PinPositionArray) //3 items max each array
        {                                                                                        //pin starting positions given between 1 to 26
            for (int x = 0; x < 3; x++)
            {
                switch (rotaSelectionArray[x])
                {
                    case rotaNames.rotaOne:
                        RotaSelection.Add(RotaOne);
                        RotaOne.SetStartingRotaPosition(PinPositionArray[x]);
                        break;
                    case rotaNames.rotaTwo:
                        RotaSelection.Add(RotaTwo);
                        RotaTwo.SetStartingRotaPosition(PinPositionArray[x]);
                        break;
                    case rotaNames.rotaThree:
                        RotaSelection.Add(RotaThree);
                        RotaThree.SetStartingRotaPosition(PinPositionArray[x]);
                        break;
                    case rotaNames.rotaFour:
                        RotaSelection.Add(RotaFour);
                        RotaFour.SetStartingRotaPosition(PinPositionArray[x]);
                        break;
                    case rotaNames.rotaFive:
                        RotaSelection.Add(RotaFive);
                        RotaFive.SetStartingRotaPosition(PinPositionArray[x]);
                        break;
                }
            }
        }

        public String encrypt_sentence(String pass_uncrypt_sentence)
        {
            pass_uncrypt_sentence = pass_uncrypt_sentence.ToUpper();// covert to upper
            Regex rgx = new Regex("[^A-Z]");
            pass_uncrypt_sentence = rgx.Replace(pass_uncrypt_sentence, "");//removes non letters
            String encrypted_message = "";
            foreach (char item in pass_uncrypt_sentence)
            {
                encrypted_message = encrypted_message + EncryptLetter(item);
            }
            return encrypted_message;
        }

        public String decrypt_sentence(String pass_encrypt_sentence)
        {
            pass_encrypt_sentence = pass_encrypt_sentence.ToUpper();// covert to upper
            Regex rgx = new Regex("[^A-Z]");
            pass_encrypt_sentence = rgx.Replace(pass_encrypt_sentence, "");//removes non letters
            String uncrypted_message = "";
            foreach (char item in pass_encrypt_sentence)
            {
                uncrypted_message = uncrypted_message + DecryptLetter(item);
            }
            return uncrypted_message;
        }

        private void rotatetRotors()//rotor 1 turns 26 turns, before 1 rotation of rotor 2
        {                           //rotor 2 turns 26 times, before 1 rotation of rotor 3
            if (rotaOneRevolutionCount < 26)
            {
                rotaOneRevolutionCount++;
                RotaSelection[0].StepRotaPosition();
            }
            else
            {
                rotaOneRevolutionCount = 1;
                RotaSelection[0].StepRotaPosition();
                if (rotaTwoRevolutionCount < 26)
                {
                    rotaTwoRevolutionCount++;
                    RotaSelection[1].StepRotaPosition();
                }
                else
                {
                    rotaTwoRevolutionCount = 1;
                    RotaSelection[1].StepRotaPosition();
                    if (rotaThreeRevolutionCount < 26)
                    {
                        rotaThreeRevolutionCount++;
                        RotaSelection[2].StepRotaPosition();
                    }
                    else
                    {
                        rotaThreeRevolutionCount = 1;
                        RotaSelection[2].StepRotaPosition();
                    }
                }
            }
        }

        private char EncryptLetter(char LetterToEncrypt) //encrypts letter (could set public or private)
        {
            char ReturnLetter = RotaSelection[0].returnEncryptedLetter(LetterToEncrypt);
            ReturnLetter = RotaSelection[1].returnEncryptedLetter(ReturnLetter);
            ReturnLetter = RotaSelection[2].returnEncryptedLetter(ReturnLetter);
            ReturnLetter = reflector(ReturnLetter);
            ReturnLetter = RotaSelection[2].returnEncryptedLetter(ReturnLetter);
            ReturnLetter = RotaSelection[1].returnEncryptedLetter(ReturnLetter);
            ReturnLetter = RotaSelection[0].returnEncryptedLetter(ReturnLetter);
            rotatetRotors();
            return ReturnLetter;
        }

        private char DecryptLetter(char LetterToDecrypt) //decrypt letter (same code really, just for ease of use)
        {                                                //since each letter is paired at any given time i.e. D would return R, R would return D
            char ReturnLetter = RotaSelection[0].returnEncryptedLetter(LetterToDecrypt);
            ReturnLetter = RotaSelection[1].returnEncryptedLetter(ReturnLetter);
            ReturnLetter = RotaSelection[2].returnEncryptedLetter(ReturnLetter);
            ReturnLetter = reflector(ReturnLetter);
            ReturnLetter = RotaSelection[2].returnEncryptedLetter(ReturnLetter);
            ReturnLetter = RotaSelection[1].returnEncryptedLetter(ReturnLetter);
            ReturnLetter = RotaSelection[0].returnEncryptedLetter(ReturnLetter);
            rotatetRotors();
            return ReturnLetter;
        }

        private char reflector(char LetterToReflect) //mimics the reflector component in engima (fixed plate to reflect electornic signal)
        {
            char[] reflectorSideOne = {'P','H','D','O','C','M','N','K','Y','Z','Q','J','S'};
            char[] reflectorSideTwo = {'A','G','U','V','F','I','T','W','E','R','L','X','B'};
            bool found = false;
            char returnLetter = '-';
            for (int z = 0; z < 13; z++)
            {
                if (reflectorSideOne[z].Equals(LetterToReflect))
                {
                    found = true;
                    returnLetter = reflectorSideTwo[z];
                    break;
                }
            }
            if (found == false)
            {
                for (int z = 0; z < 13; z++)
                {
                    if (reflectorSideTwo[z].Equals(LetterToReflect))
                    {
                        returnLetter = reflectorSideOne[z];
                        break;
                    }
                }
            }
            return returnLetter;
        }
    }

    class rota
    {
        public rotaNames RotaNumber { get; set; }
        private short RotaPoistion;
        private Char[,] AlphabetCrypto;

        public rota(rotaNames pass_RotaNumber, Char[] pass_AlphabetCrypto) //char of 26 letters of alaphabet (two columns)
        {
            AlphabetCrypto = new char[2, 13];
            RotaNumber = pass_RotaNumber;
            for (int i = 0; i < 13;i++)
            {
                    AlphabetCrypto[0, i] = pass_AlphabetCrypto[i];
            }
            int count = 13;
            for (int i = 0; i < 13; i++)
            {
                    AlphabetCrypto[1, i] = pass_AlphabetCrypto[count];
                    count++;
            }
            RotaPoistion = 1;
        }

        public void SetStartingRotaPosition(short set_rotations_1_to_26)//between 1 and 26 (26 possible positions)
        {
            for (int y = 1; y < set_rotations_1_to_26; y++)
            {
                StepRotaPosition();
            }
        }

        public void StepRotaPosition() //step the rota to next position
        {
                char charAtFront = AlphabetCrypto[0, 0];
                char charAtREAR2 = AlphabetCrypto[1, 12];
                //for (int i = 0; i < 13; i++)
                //{
                //    if (i < 12) {
                //        AlphabetCrypto[0, i] = AlphabetCrypto[0, i + 1];
                //    }
                //    else
                //    {
                //        AlphabetCrypto[0, 12] = charAtFront2;
                //        for (int z = 0; z < 13; z++)
                //        {
                //            if (z < 12)
                //            {
                //                AlphabetCrypto[1, z] = AlphabetCrypto[1, z + 1];
                //            }
                //            else
                //            {
                //                AlphabetCrypto[1, 12] = charAtFront;
                //            }
                //        }
                //    }
                //}
            for ( int i = 0; i < 13; i++)
            {
                if (i < 12)
                {
                    AlphabetCrypto[0, i] = AlphabetCrypto[0, i + 1];
                }
                else
                {
                    AlphabetCrypto[0, 12] = charAtREAR2;
                    for (int z = 12; z > -1; z--)
                    {
                        if (z > 0)
                        {
                            AlphabetCrypto[1, z] = AlphabetCrypto[1, z - 1];
                        }
                        else
                        {
                            AlphabetCrypto[1, 0] = charAtFront;
                        }
                    }
                }
            }
            RotaPoistion++;
                if (RotaPoistion==27)
                {
                      RotaPoistion = 1;
                }
        }

        public char returnEncryptedLetter(char passLetterToEncrypt) //return ecrypted letter (bilateral)
        {
            bool found = false;
            char returnLetter ='-';
            for (int z = 0; z < 13;z++)
            {
                if (AlphabetCrypto[0, z].Equals(passLetterToEncrypt))
                {
                    found = true;
                    returnLetter = AlphabetCrypto[1, z];
                    break;
                }
            }
            if (found == false)
            {
                for (int z = 0; z < 13; z++)
                {
                    if (AlphabetCrypto[1, z].Equals(passLetterToEncrypt))
                    {
                        returnLetter = AlphabetCrypto[0, z];
                        break;
                    }
                }
            }
            return returnLetter;
        }
    }
}