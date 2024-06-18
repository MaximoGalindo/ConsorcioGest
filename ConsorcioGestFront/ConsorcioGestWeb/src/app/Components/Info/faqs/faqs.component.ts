import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-faqs',
  templateUrl: './faqs.component.html',
  styleUrls: ['./faqs.component.css']
})
export class FAQSComponent {
  isAdmin: boolean = false;
  faqs = [
    // Preguntas para el usuario común
    {
      question: '¿Qué es ConsorcioGest?',
      answer: 'ConsorcioGest es una aplicación proporcionada por Maximo Galindo para gestionar consorcios. Tiene secciones tanto para administradores como para usuarios comunes.',
      showAnswer: false,
      isAdmin: false
    },
    {
      question: '¿Qué puedo hacer como usuario común?',
      answer: 'Como usuario común, puedes realizar reclamos, hacer reservas de espacios comunes, visualizar el estado de tus reclamos, ver el estado y la fecha de tus reservas, y cancelar las reservas.',
      showAnswer: false,
      isAdmin: false
    },
    {
      question: '¿Cómo realizo un reclamo?',
      answer: 'Para realizar un reclamo, inicia sesión en la aplicación, navega a la sección de reclamos y sigue las instrucciones para completar y enviar tu reclamo.',
      showAnswer: false,
      isAdmin: false
    },
    {
      question: '¿Cómo puedo hacer una reserva?',
      answer: 'Para hacer una reserva, inicia sesión en la aplicación, dirígete a la sección de reservas, elige el espacio común que deseas reservar, selecciona la fecha y hora, y confirma tu reserva.',
      showAnswer: false,
      isAdmin: false
    },
    {
      question: '¿Cómo puedo visualizar el estado de mis reclamos?',
      answer: 'Puedes visualizar el estado de tus reclamos en la sección de reclamos de la aplicación. Aquí verás una lista de tus reclamos junto con su estado actual.',
      showAnswer: false,
      isAdmin: false
    },
    {
      question: '¿Cómo puedo cancelar una reserva?',
      answer: 'Para cancelar una reserva, ve a la sección de reservas, selecciona la reserva que deseas cancelar y sigue las instrucciones para confirmar la cancelación.',
      showAnswer: false,
      isAdmin: false
    },

    // Preguntas para el administrador
    {
      question: '¿Qué puede hacer un administrador en ConsorcioGest?',
      answer: 'Un administrador puede registrar un consorcio, detallar bloques del edificio, agregar espacios comunes, y gestionar reclamos, reservas, usuarios, encuestas y estadísticas.',
      showAnswer: false,
      isAdmin: true
    },
    {
      question: '¿Cómo registro un consorcio?',
      answer: 'Para registrar un consorcio, el administrador debe ingresar datos como nombre, ubicación y CUIT del consorcio en la sección correspondiente de la aplicación.',
      showAnswer: false,
      isAdmin: true
    },
    {
      question: '¿Cómo detallo los bloques del edificio?',
      answer: 'El administrador puede detallar los bloques del edificio ingresando los datos necesarios en la sección de bloques de la aplicación, lo que generará automáticamente la estructura del edificio.',
      showAnswer: false,
      isAdmin: true
    },
    {
      question: '¿Cómo agrego espacios comunes?',
      answer: 'Para agregar espacios comunes, el administrador debe ir a la sección de espacios comunes en la aplicación y completar los detalles necesarios para cada espacio.',
      showAnswer: false,
      isAdmin: true
    },
    {
      question: '¿Cómo gestiono los reclamos?',
      answer: 'El administrador puede visualizar y filtrar los reclamos por estado, cambiar su estado según sea necesario y agregar observaciones desde la sección de reclamos.',
      showAnswer: false,
      isAdmin: true
    },
    {
      question: '¿Cómo gestiono las reservas?',
      answer: 'En la sección de reservas, el administrador puede visualizar todas las reservas en una parrilla, ver la información de quién realizó la reserva y los horarios, y cancelarlas manualmente si es necesario.',
      showAnswer: false,
      isAdmin: true
    },
    {
      question: '¿Cómo gestiono a los usuarios?',
      answer: 'El administrador tiene control total sobre los usuarios, pudiendo editarlos, darlos de baja o habilitarlos desde la sección de usuarios.',
      showAnswer: false,
      isAdmin: true
    },
    {
      question: '¿Cómo visualizo las encuestas?',
      answer: 'En la sección de encuestas, el administrador puede ver tanto las preguntas como las respuestas proporcionadas por los usuarios y aplicar filtros según sea necesario.',
      showAnswer: false,
      isAdmin: true
    },
    {
      question: '¿Cómo puedo ver las estadísticas del consorcio?',
      answer: 'El administrador puede ver las estadísticas generales del estado del consorcio y aplicar filtros por fechas en la sección de estadísticas.',
      showAnswer: false,
      isAdmin: true
    }
  ];

  filteredFaqs = this.faqs;

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      console.log(data);      
      this.isAdmin = data['isAdmin'];
      this.filterFaqs();
    });
  }

  filterFaqs() {
    if (this.isAdmin) {
      this.filteredFaqs = this.faqs.filter(faq => faq.isAdmin);
    } else {
      this.filteredFaqs = this.faqs.filter(faq => !faq.isAdmin);
    }
  }

  toggleAnswer(faq: any) {
    faq.showAnswer = !faq.showAnswer;
  }

}
