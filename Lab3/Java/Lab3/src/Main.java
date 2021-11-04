import model.Account;
import task1.Task1;
import utils.CasinoRoyale;

public class Main {

    public static void main(String[] args) {
        // https://docs.google.com/document/d/1HY7Dl-5itYD3C_gkueBvvBFpT4CecGPiR30BsARlTpQ/edit

        Account account = CasinoRoyale.createAcc();
        System.out.println(account);

        Task1.run(account);
    }
}
