package br.com.avancertecnologia.util.task;

import android.app.Activity;

import java.text.SimpleDateFormat;
import java.util.Timer;
import java.util.TimerTask;

import br.com.avancertecnologia.util.DateUtil;
import br.com.avancertecnologia.util.task.listeners.TaskListener;

public class CronometroTask extends TimerTask {

    private TaskListener taskListener;

    private Activity act;
    private long tempo;
    private SimpleDateFormat simpleDateFormat;
    private boolean isReverso;
    private boolean isFinish;
    private boolean isExecuteTermino;
    private double porcentualInicial;
    private double porcentual;

    private Object locker = new Object();

    public CronometroTask(Activity act, boolean isReverso) {
        super();
        this.Init(act, isReverso);
    }

    private void Init(Activity act, boolean isReverso) {
        this.act = act;
        this.tempo = 0;
        this.simpleDateFormat = new SimpleDateFormat("HH:mm:ss");
        this.isReverso = isReverso;
        this.isFinish = true;
        this.isExecuteTermino = true;
        Timer timer = new Timer();
        timer.scheduleAtFixedRate(this, 100, 100);
    }

    public void start() {
        synchronized (locker) {
            if (this.tempo <= 0 && isReverso) {
                throw new ExceptionInInitializerError("O tempo não pode ser zero!");
            }
            this.isFinish = false;
            this.isExecuteTermino = false;
        }
    }

    public boolean isRunning() {
        return this.isFinish;
    }

    public void setTime(long tempo) {
        double segundos = tempo / 1000;
        if (segundos != 0) {
            this.porcentualInicial = (100 / segundos) / 10;
        }
        this.tempo = tempo;
    }

    public long getTime() {
        return this.tempo;
    }

    public void addTime(long tempo) {
        setTime(getTime() + tempo);
    }

    private void termine() {
        this.porcentual = 0;
        this.porcentualInicial = 0;
        this.isFinish = true;
        this.isExecuteTermino = true;
    }

    public void stop() {
        this.termine();
        this.taskListener.termino();
    }

    @Override
    public void run() {
        this.act.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                synchronized (locker) {
                    if (isFinish) {
                        return;
                    }
                    if (taskListener != null) {
                        if (!isFinish) {
                            taskListener.atualizar(format(tempo));
                        }
                        if (isReverso) {
                            if (isFinish) {
                                termine();
                            } else {
                                taskListener.atualizar(format(tempo), (int) porcentual);
                            }
                        }
                    }
                    if (isReverso) {
                        removerSegundo();
                    } else {
                        adicionarSegundo();
                    }
                    porcentual += porcentualInicial;
                }
            }
        });
    }

    @Override
    public boolean cancel() {
        termine();
        return super.cancel();
    }

    @Override
    public long scheduledExecutionTime() {
        return super.scheduledExecutionTime();
    }

    public void setTaskListener(TaskListener taskListener) {
        this.taskListener = taskListener;
    }

    private void removerSegundo() {
        synchronized (locker) {
            if (this.tempo == 0) {
                //this.isFinish = true;
                if (this.taskListener != null && !this.isExecuteTermino) {
                    //this.isExecuteTermino = true;
                    this.termine();
                    this.taskListener.termino();
                }
                return;
            } else {
                if (this.tempo <= 0) {
                    return;
                }
                this.tempo -= 100;
                if (this.tempo <= 0) {
                    this.tempo = 0;
                }
            }
        }
    }

    private void adicionarSegundo() {
        throw new IllegalArgumentException("Metodo não implementado");
    }

    private String format(long tempo){
        long time = tempo;
        int hora = DateUtil.FromHour(time);
        time -= hora * 1000 * 60 * 60;
        int minuto = DateUtil.FromMinute(time);
        time -= minuto * 1000 * 60;
        int segundo = DateUtil.FromSegunds(time);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(hora < 10 ? "0" + hora : hora);
        stringBuilder.append(":");
        stringBuilder.append(minuto < 10 ? "0" + minuto : minuto);
        stringBuilder.append(":");
        stringBuilder.append(segundo < 10 ? "0" + segundo : segundo);
        return stringBuilder.toString();
    }

}
