package br.com.avancertecnologia.gui;

import android.content.Context;
import android.content.res.TypedArray;
import android.graphics.Typeface;
import android.util.AttributeSet;
import android.view.View;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import br.com.avancertecnologia.R;

/**
 * Botão personalizado, podendo inserir um Titulo e um SubTitutlo
 */
public class CustomButton extends LinearLayout {

    private TextView txtTitle;
    private TextView txtSubTitle;
    private ImageView imageView;

    private boolean isEnabled;

    public CustomButton(Context context, AttributeSet attrs) {
        super(context, attrs);
        this.initComponents(context, attrs, 0);
    }

    public CustomButton(Context context, AttributeSet attrs, int defStyle) {
        super(context, attrs, defStyle);
        this.initComponents(context, attrs, defStyle);
    }

    private void initComponents(Context context, AttributeSet attrs, int defStyle) {
        View.inflate(context, R.layout.sample_custom_button, this);
        this.setBackgroundResource(R.drawable.custom_button_enable);
        this.setClickable(true);
        this.setFocusable(true);
        this.txtTitle = findViewById(R.id.title);
        this.txtSubTitle = findViewById(R.id.subTitle);
        this.imageView = findViewById(R.id.imageView);
        this.setAlpha(0.8f);
        this.initStyledAttribute(context, attrs, defStyle);
    }

    private void initStyledAttribute(Context context, AttributeSet attrs, int defStyle) {
        TypedArray a = context.obtainStyledAttributes(attrs, R.styleable.CustomButton, defStyle, 0);
        setTitle(a.getString(R.styleable.CustomButton_titulo));
        setSubTitle(a.getString(R.styleable.CustomButton_subTitulo));
        setFontSizeTitle(a.getDimension(R.styleable.CustomButton_textSizeTitulo, 10f));
        setFontSizeSubTitle(a.getDimension(R.styleable.CustomButton_textSizeSubTitulo, 10f));
        setFontStyleTitle(a.getInt(R.styleable.CustomButton_textStyleTitulo, 0));
        setFontStyleSubTitle(a.getInt(R.styleable.CustomButton_textStyleSubTitulo, 0));
        setImage(a.getResourceId(R.styleable.CustomButton_imagem, 0));
        setImageTint(a.getColor(R.styleable.CustomButton_imagemTint, 0));
    }

    @Override
    public void setEnabled(boolean enabled) {
        super.setEnabled(enabled);
        if (enabled) {
            this.setAlpha(0.8f);
        } else {
            this.setAlpha(0.4f);
        }
    }

    @Override
    public void setPressed(boolean pressed) {
        super.setPressed(pressed);
        if(!this.isEnabled()){
            return;
        }
        if (pressed) {
            this.setAlpha(1f);
        } else {
            this.setAlpha(0.8f);
        }
    }

    @Override
    public void setFocusable(boolean focusable) {
        super.setFocusable(focusable);
        if(!this.isEnabled()){
            return;
        }
        if (focusable) {
            this.setAlpha(0.9f);
        } else {
            this.setAlpha(0.8f);
        }
    }

    public void setTitle(CharSequence text) {
        if (txtTitle != null) {
            txtTitle.setText(text);
        }
    }

    public void setSubTitle(CharSequence text) {
        if (txtSubTitle != null) {
            txtSubTitle.setText(text);
        }
    }

    public void setFontSizeTitle(float size) {
        if (txtTitle != null) {
            txtTitle.setTextSize(size);
        }
    }

    public void setFontSizeSubTitle(float size) {
        if (txtSubTitle != null) {
            //txtSubTitle.setTextSize(size);
        }
    }

    public void setFontStyleTitle(int result) {
        if (txtTitle != null) {
            switch (result) {
                case 0:
                    txtTitle.setTypeface(Typeface.DEFAULT);
                    break;
                case 1:
                    txtTitle.setTypeface(Typeface.DEFAULT_BOLD);
                    break;
            }
        }
    }

    public void setFontStyleSubTitle(int result) {
        if (txtSubTitle != null) {
            switch (result) {
                case 0:
                    txtSubTitle.setTypeface(Typeface.DEFAULT);
                    break;
                case 1:
                    txtSubTitle.setTypeface(Typeface.DEFAULT_BOLD);
                    break;
            }
        }
    }

    public void setImage(int resource) {
        if (this.imageView != null && resource != 0) {
            this.imageView.setImageResource(resource);
        }
    }

    public void setImageTint(int color) {
        if (this.imageView != null && color != 0) {
            this.imageView.setColorFilter(color);
        }
    }

}
