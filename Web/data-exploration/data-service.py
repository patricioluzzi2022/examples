import socketio
import eventlet
import random
import sounddevice as sd
import numpy as np
from scipy import signal
from scipy.fft import fft, fftfreq

# Creamos el servidor Socket.IO
sio = socketio.Server(cors_allowed_origins='*')
app = socketio.WSGIApp(sio)

layout_frequency_range = {
    "title": "frequency_threshold",
    "type": "line",
    "intersect": False,
    "axis": [
        {
            "name": "x",
            "title": "measurements (#)",
            "range": ['11', '10', '9', '8', '7', '6', '5', '4', '3', '2', '1', '0'],
            "lcolor": "rgb(200, 200, 200)",
            "mirror": False
        },
        {
            "name": "y",
            "title": "frequency (Hz)",
            "range": [],
            "lcolor": "rgb(200, 200, 200)",
            "mirror": False
        },
    ],
    "datasets": [
        {
            "label": "Microphone Frequencies",
            "data": [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            "borderColor": "rgb(217, 141, 54)",
            "backgroundColor": "rgba(75, 192, 192, 0.2)",
            "fill": True,
            "tension": 0.883
        }
    ],
    "range": [0, 340], # approximate range 
    "plot_bg_color": "white",
    "paper_bg_color": "white",
    "analisis": []
}

layout_magnitudes_per_freq = {
    "title": "layout_magnitudes_per_freq",
    "type": "line",
    "intersect": False,
    "axis": [
        {
            "name": "x",
            "title": "measurements (#)",
            "range": ['11', '10', '9', '8', '7', '6', '5', '4', '3', '2', '1', '0'],
            "lcolor": "rgb(200, 200, 200)",
            "mirror": False
        },
        {
            "name": "y",
            "title": "maginitude (db)",
            "range": [],
            "lcolor": "rgb(200, 200, 200)",
            "mirror": False
        },
    ],
    "datasets": [
        {
            "label": "Microphone Frequencies",
            "data": [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            "borderColor": "rgb(200, 200, 200)",
            "backgroundColor": "rgba(75, 192, 192, 0.2)",
            "fill": True,
            "tension": 0.883
        }
    ],
    "range": [0, 130], # verificar 
    "plot_bg_color": "white",
    "paper_bg_color": "white",
    "analisis": []
}

@sio.event
def connect(sid, environ):
    microphone = microphone_status()
    sio.emit('connected-event', {'message': '¡Conexión establecida!', 'microphone': str(microphone)}, to=sid)

@sio.on('layout-event')
def get_layout(sid, id):
    if(id=="GChart"):
        sio.emit('layout-callback-event', {'layout': layout_frequency_range }, to=sid)
    if(id=="MGChart"):
        sio.emit('layout-callback-event', {'layout': layout_magnitudes_per_freq }, to=sid)

@sio.on('frequency-analisis-event')
def handle_get_frequency_analize_event(sid, data):
    analysis = analyze_microphone_frequencies(2, 48000, False)
    sio.emit('frequency-analisis-callback-event', {'analysis': analysis}, to=sid)    
    
def capture_audio_chunk(duration=1.0, sample_rate=44100):
    """
    Captura un fragmento de audio del micrófono.
    
    Args:
        duration: Duración de captura en segundos (default: 1.0)
        sample_rate: Frecuencia de muestreo en Hz (default: 44100)
    
    Returns:
        numpy array con los datos de audio
    """
    audio_data = sd.rec(int(duration * sample_rate), samplerate=sample_rate, channels=1, dtype='float32')
    sd.wait()
    return audio_data.flatten(), sample_rate

def compute_fft(audio_data, sample_rate):
    """
    Calcula la Transformada Rápida de Fourier (FFT) del audio.
    
    Args:
        audio_data: Array de datos de audio
        sample_rate: Frecuencia de muestreo
    
    Returns:
        frequencies: Array de frecuencias
        magnitudes: Array de magnitudes (amplitud)
    """
    # Aplicar ventana de Hamming para reducir artefactos
    windowed = audio_data * np.hamming(len(audio_data))
    
    # Calcular FFT
    fft_result = fft(windowed)
    magnitudes = np.abs(fft_result)
    
    # Obtener solo la mitad positiva del espectro
    magnitudes = magnitudes[:len(magnitudes)//2]
    
    # Calcular frecuencias
    frequencies = fftfreq(len(windowed), 1/sample_rate)[:len(magnitudes)]
    
    return frequencies, magnitudes

def analyze_frequencies(frequencies, magnitudes, top_n=5):
    """
    Analiza el espectro de frecuencias.
    
    Args:
        frequencies: Array de frecuencias
        magnitudes: Array de magnitudes
        top_n: Número de frecuencias dominantes a retornar
    
    Returns:
        dict con análisis de frecuencias
    """
    # Frecuencia dominante
    dominant_idx = np.argmax(magnitudes)
    dominant_freq = frequencies[dominant_idx]
    dominant_mag = magnitudes[dominant_idx]
    
    # Top N frecuencias
    top_indices = np.argsort(magnitudes)[-top_n:][::-1]
    top_freqs = frequencies[top_indices]
    top_mags = magnitudes[top_indices]
    
    # Estadísticas
    max_mag = np.max(magnitudes)
    min_mag = np.min(magnitudes)
    mean_mag = np.mean(magnitudes)
    
    return {
        'dominant_frequency': dominant_freq,
        'dominant_magnitude': dominant_mag,
        'top_frequencies': list(zip(top_freqs, top_mags)),
        'max_magnitude': max_mag,
        'min_magnitude': min_mag,
        'mean_magnitude': mean_mag
    }

def analyze_microphone_frequencies(duration=2.0, sample_rate=440, visualize=True):
    """
    Función principal que captura y analiza frecuencias del micrófono.
    
    Args:
        duration: Duración de captura en segundos (default: 2.0)
        sample_rate: Frecuencia de muestreo en Hz (default: 44100)
        visualize: Si mostrar gráficos (default: True)
    
    Returns:
        dict con resultados del análisis
    """
    #print(f"\n=== ANALIZADOR DE FRECUENCIAS DEL MICRÓFONO ===")
    #print(f"Parámetros: Duración={duration}s, Sample Rate={sample_rate}Hz\n")
    
    # Paso 1: Capturar audio
    audio_data, sr = capture_audio_chunk(duration, sample_rate)
    
    # Paso 2: Calcular FFT
    frequencies, magnitudes = compute_fft(audio_data, sr)
    
    # Paso 3: Analizar
    analysis = analyze_frequencies(frequencies, magnitudes, top_n=6)
    
    return analysis

def analyze_frequency_sequence(audio_data, sample_rate, num_frequencies=2, threshold=20):
    """
    Analiza frecuencias y devuelve secuencia de máximo N frecuencias dominantes.
    
    Args:
        audio_data: Array de datos de audio
        sample_rate: Frecuencia de muestreo
        num_frequencies: Número máximo de frecuencias (default: 2)
        threshold: Umbral mínimo de magnitud (default: 20)
    
    Returns:
        list: Array con máximo N frecuencias [freq1, freq2] o menos
    """
    # Calcular FFT
    frequencies, magnitudes = compute_fft(audio_data, sample_rate)
    
    # Detectar picos locales (máximos relativos)
    peaks = []
    for i in range(1, len(magnitudes) - 1):
        if (magnitudes[i] > magnitudes[i-1] and 
            magnitudes[i] > magnitudes[i+1] and 
            magnitudes[i] > threshold):
            peaks.append({
                'frequency': int(frequencies[i]),
                'magnitude': magnitudes[i],
                'index': i
            })
    
    if not peaks:
        return []
    
    # Ordenar por magnitud y tomar máximo N
    top_peaks = sorted(peaks, key=lambda x: x['magnitude'], reverse=True)[:num_frequencies]
    
    # Retornar solo las frecuencias
    return [p['frequency'] for p in top_peaks]

def microphone_status():
    try:
        duration = 2
        fs = 48000

        audio = sd.rec(
            int(duration * fs),
            samplerate=fs,
            channels=1,
            dtype='float32'
        )
        sd.wait()

        volumen = np.linalg.norm(audio)
        return volumen > 0.01
    except Exception as e:
        print(e)
        return False

if __name__ == '__main__':
    # Ejecutamos el servidor en el puerto 5000
    eventlet.wsgi.server(eventlet.listen(('', 5000)), app)
    