package br.com.avancertecnologia.model.drink;

import java.io.Serializable;

import br.com.avancertecnologia.BuildConfig;
import br.com.avancertecnologia.model.drink.enums.Tipo;
import br.com.avancertecnologia.util.task.CronometroTask;

public class Nivel implements Serializable{

    private String Nome;

    private int Nivel;

    private int NivelMaximo;

    private double Valor;

    private double Custo;

    private double CustoInicial;

    private long Tempo;

    private long DefaultTempo;

    private boolean IsVendaAutomatica;

    private Tipo Tipo;

    private transient CronometroTask cronometroTask;

    private boolean isCronometroStart;

    private boolean ativo;

    public Nivel(Tipo tipo) {
        this.Tipo = tipo;
    }

    //region get set
    public String getNome() {
        return Nome;
    }

    public void setNome(String nome) {
        Nome = nome;
    }

    public int getNivel() {
        return Nivel;
    }

    public void setNivel(int nivel) {
        Nivel = nivel;
    }

    public double getValor() {
        return Valor;
    }

    public int getNivelMaximo() {
        return NivelMaximo;
    }

    public void setNivelMaximo(int nivelMaximo) {
        NivelMaximo = nivelMaximo;
    }

    public void setValor(double valor) {
        Valor = valor;
    }

    public double getCusto() {
        return Custo;
    }

    public void setCusto(double custo) {
        Custo = custo;
    }

    public double getCustoInicial() {
        return CustoInicial;
    }

    public void setCustoInicial(double custoInicial) {
        CustoInicial = custoInicial;
    }

    public long getTempo() {
        return Tempo;
    }

    public void setTempo(long tempo) {
        Tempo = tempo;
    }

    public long getDefaultTempo() {
        return DefaultTempo;
    }

    public void setDefaultTempo(long defaultTempo) {
        DefaultTempo = defaultTempo;
    }

    public boolean isVendaAutomatica() {
        return IsVendaAutomatica;
    }

    public void setVendaAutomatica(boolean vendaAutomatica) {
        IsVendaAutomatica = vendaAutomatica;
    }

    public Tipo getTipo() {
        return Tipo;
    }

    public void setCronometroTask(CronometroTask cronometroTask){
        this.cronometroTask = cronometroTask;
    }

    public CronometroTask getCronometroTask() {
        return cronometroTask;
    }

    public boolean isCronometroStart() {
        return isCronometroStart;
    }

    public void setCronometroStart(boolean cronometroStart) {
        isCronometroStart = cronometroStart;
    }

    public boolean isAtivo() {
        return ativo;
    }

    public void setAtivo(boolean ativo) {
        this.ativo = ativo;
    }

    //endregion

    public boolean evoluir(Personagem personagem){
        double custoInicial = this.getCustoInicial();
        double nivelMellhoria = this.getNivel() + 1;
        double custo;
        if(BuildConfig.DEBUG){
            custo = 0.01;
        }
        else{
            custo = custoInicial * nivelMellhoria;
        }
        if(personagem.getSaldo() >= custo && getNivel() < getNivelMaximo()){
            personagem.setSaldo(personagem.getSaldo() - custo);
            this.setNivel(this.getNivel() + 1);
            switch (getTipo()){
                case Agua:
                    this.setValor(this.getValor() + 0.0100);
                    break;
                case Gelo:
                    this.setValor(this.getValor() + 0.0125);
                    break;
                case Limao:
                    this.setValor(this.getValor() + 0.0150);
                    break;
                case Laranja:
                    this.setValor(this.getValor() + 0.0175);
                    break;
                case Barraco_Madeira:
                    this.setValor(this.getValor() + 0.01);
                    break;
                case Barraco_Elegante:
                    this.setValor(this.getValor() + 0.03);
                    break;
                case Loja_Pequena:
                    this.setValor(this.getValor() + 0.09);
                    break;
                case Loja_Media:
                    this.setValor(this.getValor() + 0.27);
                    break;
                case Loja_Grande:
                    this.setValor(this.getValor() + 0.81);
                    break;
                case Empresa_Pequena:
                    this.setValor(this.getValor() + 2.43);
                    break;
                case Empresa_Media:
                    this.setValor(this.getValor() + 7.29);
                    break;
                case Empresa_Grande:
                    this.setValor(this.getValor() + 21.87);
                    break;
            }
            return true;
        }
        else{
            return false;
        }
    }

    public double vender(){
        return this.getValor();
    }

    public void diminuirTempo(){
        long tempo = getDefaultTempo();
        long result = (long) (tempo * 0.02d);
        setTempo(getTempo()-result);
    }

}
